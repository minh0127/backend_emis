using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MISA.Web06.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Kiểm tra trùng số hiệu cán bộ
        /// </summary>
        /// <param name="employeeCode">Số hiệu cán bộ</param>
        /// <returns>true: Nếu trùng, false: Không trùng</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        public bool CheckEmployeeCodeExist(string employeeCode)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employeeCode);

                var res = MySqlConnection.QueryFirstOrDefault(sqlCommand, param: parameters);

                if (res != null)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Lấy thông tin giáo viên theo ID
        /// </summary>
        /// <param name="id">ID của giáo viên</param>
        /// <returns>Thông tin giáo viên theo ID</returns>
        /// CreatedBy: MINHBUI (11/08/2022)
        public Employee GetEmployeeByID(Guid EmployeeID)
        {
            
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT * FROM View_Employee WHERE EmployeeID = @{TableName}ID";
                var parameters = new DynamicParameters();
                parameters.Add($"@{TableName}ID", EmployeeID);

                var res = MySqlConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);

                return res;
            }
        }

        /// <summary>
        /// Phân trang giáo viên
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang</param>
        /// <param name="pageSize">Kích cỡ trang</param>
        /// <param name="filter">Từ khoá lọc trang</param>
        /// <returns>Danh sách giáo viên đã phân trang</returns>
        public object GetEmployeePaging(int pageIndex, int pageSize, string? filter = "")
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"Proc_GetEmployeePaging";
                var parameters = new DynamicParameters();
                parameters.Add("@Filter", filter);
                parameters.Add("@PageIndex", pageIndex);
                parameters.Add("@PageSize", pageSize);
                parameters.Add("@TotalRecord", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@RecordStart", direction: System.Data.ParameterDirection.Output);
                parameters.Add("@RecordEnd", direction: System.Data.ParameterDirection.Output);

                var res = MySqlConnection.Query<Employee>(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

                int totalRecord = parameters.Get<int>("@TotalRecord");
                int recordStart = parameters.Get<int>("@RecordStart");
                int recordEnd = parameters.Get<int>("@RecordEnd");

                return new
                {
                    TotalRecord = totalRecord,
                    RecordStart = recordStart,
                    RecordEnd = recordEnd,
                    Data = res
                };
            }
        }

        /// <summary>
        /// Lấy danh sách cán bộ giáo viên sắp giảm dần theo Số hiệu cán bộ
        /// </summary>
        /// <returns>Danh sách cán bộ giáo viên sắp giảm dần theo Số hiệu cán bộ</returns>
        /// CreatedBy: MINHBUI (11/08/2022)
        public IEnumerable<Employee> GetEmployees()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT * FROM View_Employee";

                var res = MySqlConnection.Query<Employee>(sqlCommand);

                return res;
            }
        }

        public string GetNewEmployeeCode()
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"SELECT CONCAT('SHCB', (MAX(SUBSTRING(ve.EmployeeCode,5))+1)) FROM View_Employee ve;";

                var newEmployeeCode = MySqlConnection.QueryFirstOrDefault<string>(sqlCommand);

                return newEmployeeCode;
            }
        }

        public int InsertEmployee(Employee employee)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                MySqlConnection.Open();
                using (MySqlTransaction tran = MySqlConnection.BeginTransaction())
                {
                    try
                    {
                        var sqlCommand = $"Proc_InsertEmployee";

                        var employeeID = Guid.NewGuid();
                        employee.EmployeeID = employeeID;
                        var res = MySqlConnection.Execute(sqlCommand, param: employee, transaction: tran, commandType: System.Data.CommandType.StoredProcedure);

                        string[] departmentIDs = employee.DepartmentID.Split(", ");
                        var insertEmployeeDepartmentCommand = $"Proc_InsertEmployeeDepartment";
                        foreach (var departmentID in departmentIDs)
                        {
                            var param = new EmployeeDepartment(employee.EmployeeID, Guid.Parse(departmentID));
                            MySqlConnection.Execute(insertEmployeeDepartmentCommand, param: param, transaction: tran, commandType: System.Data.CommandType.StoredProcedure);
                        }

                        string[] subjectIDs = employee.SubjectID.Split(", ");
                        var insertEmployeeSubjectCommand = $"Proc_InsertEmployeeSubject";
                        foreach (var subjectID in subjectIDs)
                        {
                            var param = new EmployeeSubject(employee.EmployeeID, Guid.Parse(subjectID));
                            MySqlConnection.Execute(insertEmployeeSubjectCommand, param: param, transaction: tran, commandType: System.Data.CommandType.StoredProcedure);
                        }

                        tran.Commit();
                        return res;
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }

                }
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            using (MySqlConnection = new MySqlConnection(ConnectionString))
            {
                MySqlConnection.Open();
                using (MySqlTransaction tran = MySqlConnection.BeginTransaction())
                {
                    try
                    {
                        var sqlCommand = $"Proc_UpdateEmployee";

                        var res = MySqlConnection.Execute(sqlCommand, param: employee, transaction: tran, commandType: System.Data.CommandType.StoredProcedure);

                        // Xoá hết EmployeeDepartment có EmployeeID = employee.EmployeeID
                        var deleteEmployeeDepartmentCommand = "DELETE FROM EmployeeDepartment WHERE EmployeeID = @EmployeeID";
                        var parameters = new DynamicParameters();
                        parameters.Add("@EmployeeID", employee.EmployeeID);
                        MySqlConnection.Execute(deleteEmployeeDepartmentCommand, param: parameters, transaction: tran);

                        // Xoá hết EmployeeSubject có EmployeeID = employee.EmployeeID
                        var deleteEmployeeSubjectCommand = "DELETE FROM EmployeeSubject WHERE EmployeeID = @EmployeeID";
                        MySqlConnection.Execute(deleteEmployeeSubjectCommand, param: parameters, transaction: tran);

                        // Thêm lại dữ liệu update
                        var insertEmployeeDepartmentCommand = $"Proc_InsertEmployeeDepartment";

                        string[] departmentIDs = employee.DepartmentID.Split(", ");
                        foreach (var departmentID in departmentIDs)
                        {
                            var param = new EmployeeDepartment(employee.EmployeeID, Guid.Parse(departmentID));
                            MySqlConnection.Execute(insertEmployeeDepartmentCommand, param: param, transaction: tran, commandType: System.Data.CommandType.StoredProcedure);
                        }

                        var insertEmployeeSubjectCommand = $"Proc_InsertEmployeeSubject";
                        string[] subjectIDs = employee.SubjectID.Split(", ");
                        foreach (var subjectID in subjectIDs)
                        {
                            var param = new EmployeeSubject(employee.EmployeeID, Guid.Parse(subjectID));
                            MySqlConnection.Execute(insertEmployeeSubjectCommand, param: param, transaction: tran, commandType: System.Data.CommandType.StoredProcedure);
                        }

                        tran.Commit();
                        return res;
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }

                }
            }
        }

    }
}
