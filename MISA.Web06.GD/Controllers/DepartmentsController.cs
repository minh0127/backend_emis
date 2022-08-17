using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using MISA.Web06.GD.Entities;


namespace MISA.Web06.GD.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Field
        string _connectionString = "Host=3.0.89.182;" +
                    " Port=3306;" +
                    " Database=MISA.WEB06.MINHBUI;" +
                    " User Id=dev;" +
                    " Password=12345678;";
        MySqlConnection _connection;
        #endregion

        /// <summary>
        /// Hàm khởi tạo: Kết nối database
        /// </summary>
        /// CreatedBy: MINHBUI (02/08/2022)
        public DepartmentsController()
        {
            _connection = new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// CreatedBy: MINHBUI (02/08/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Thực thi lấy dữ liệu
                var sqlCommand = "SELECT * FROM Department";

                // Trả về dữ liệu cho client
                var data = _connection.Query<Department>(sqlCommand);

                return Ok(data);
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Danh sách phòng ban</returns>
        /// CreatedBy: MINHBUI (02/08/2022)
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            try
            {
                // Thực thi lấy dữ liệu
                var sqlCommand = $"SELECT * FROM Department WHERE DepartmentID = @DepartmentID";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@DepartmentID", id.ToString());

                // Trả về dữ liệu cho client
                var department = _connection.QueryFirstOrDefault<Department>(sqlCommand, param: dynamicParams);

                return Ok(department);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm phòng ban mới
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        /// CreatedBy: MINHBUI (02/08/2022)
        [HttpPost]
        public IActionResult Post(Department department)
        {
            try
            {
                // Validate dữ liệu
                // Tên phòng ban không được phép để trống
                if (string.IsNullOrEmpty(department.DepartmentName))
                {
                    var res = new
                    {
                        devMsg = "Dữ liệu đầu vào không hợp lệ",
                        userMsg = "Tên phòng ban không được phép để trống."
                    };
                    return StatusCode(400, res);
                }

                // Thực thi thêm dữ liệu
                var rowAdd = 0;
                // Mở kết nối
                _connection.Open();
                // Thực hiện transaction: Rollback khi có lỗi xảy ra
                using (var transaction = _connection.BeginTransaction())
                {
                    var sqlCommand = "Proc_InsertDepartment";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@m_DepartmentName", department.DepartmentName);
                    rowAdd = _connection.Execute(sqlCommand, param: dynamicParams, transaction:transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                }

                // Trả về dữ liệu cho client
                return StatusCode(201, rowAdd);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>Mã lỗi</returns>
        /// CreatedBy: MINHBUI (02/08/2022)
        private IActionResult HandleException(Exception ex)
        {
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được hỗ trợ."
            };
            return StatusCode(500, ex.Message);
        }

    }
}
