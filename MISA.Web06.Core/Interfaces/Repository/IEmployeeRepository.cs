using MISA.Web06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Kiểm tra trùng số hiệu cán bộ
        /// </summary>
        /// <param name="employeeCode">Số hiệu cán bộ</param>
        /// <returns>true: Nếu trùng, false: Không trùng</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        bool CheckEmployeeCodeExist(string employeeCode);

        /// <summary>
        /// Lấy danh sách cán bộ giáo viên sắp giảm dần theo Số hiệu cán bộ
        /// </summary>
        /// <returns>Danh sách cán bộ giáo viên sắp giảm dần theo Số hiệu cán bộ</returns>
        /// CreatedBy: MINHBUI (11/08/2022)
        public IEnumerable<Employee> GetEmployees();

        /// <summary>
        /// Lấy thông tin giáo viên theo ID
        /// </summary>
        /// <param name="id">ID của giáo viên</param>
        /// <returns>Thông tin giáo viên theo ID</returns>
        /// CreatedBy: MINHBUI (11/08/2022)
        public Employee GetEmployeeByID(Guid EmployeeID);

        /// <summary>
        /// Phân trang giáo viên
        /// </summary>
        /// <param name="pageIndex">Chỉ số trang</param>
        /// <param name="pageSize">Kích cỡ trang</param>
        /// <param name="filter">Từ khoá lọc trang</param>
        /// <returns>Danh sách giáo viên đã phân trang</returns>
        public object GetEmployeePaging(int pageIndex, int pageSize, string? filter = "");

        /// <summary>
        /// Lấy Số hiệu cán bộ mới
        /// </summary>
        /// <returns>Số hiệu cán bộ mới</returns>
        public string GetNewEmployeeCode();

        /// <summary>
        /// Thêm mới giáo viên
        /// </summary>
        /// <param name="employee">Giáo viên</param>
        /// <returns>Số lượng giáo viên thêm mới thành công</returns>
        public int InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa giáo viên
        /// </summary>
        /// <param name="employee">Giáo viên</param>
        /// <returns>Số lượng giáo viên sửa thành công</returns>
        public int UpdateEmployee(Employee employee);
    }
}
