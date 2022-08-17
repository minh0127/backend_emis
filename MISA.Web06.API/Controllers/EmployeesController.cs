using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web06.Core.Exceptions;
using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Interfaces.Services;
using MISA.Web06.Core.Models;

namespace MISA.Web06.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        IEmployeeService _service;
        IEmployeeRepository _repository;
        public EmployeesController(IEmployeeService service, IEmployeeRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu giáo viên sắp giảm dần theo Số hiệu cán bộ
        /// </summary>
        /// <returns>Danh sách giáo viên sắp giảm dần theo Số hiệu cán bộ</returns>
        /// CreatedBy: MINHBUI (11/08/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Trả về dữ liệu cho client
                var data = _repository.GetEmployees();

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy dữ liệu giáo viên theo ID
        /// </summary>
        /// <param name="id">ID giáo viên cần lấy dữ liệu</param>
        /// <returns>Dữ liệu giáo viên có ID = id</returns>
        /// CreatedBy: MINHBUI (11/08/2022)
        [HttpGet("{EmployeeID}")]
        public IActionResult GetByID(Guid EmployeeID)
        {
            try
            {
                // Trả về dữ liệu cho client
                var data = _repository.GetEmployeeByID(EmployeeID);

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                // Trả về dữ liệu cho client
                var data = _repository.GetNewEmployeeCode();

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        [HttpGet("filter")]
        public IActionResult GetEmployeePaging(int pageIndex, int pageSize, string? filter)
        {
            try
            {
                var data = _repository.GetEmployeePaging(pageIndex, pageSize, filter);
                return Ok(data);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới giáo viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>1: Nếu thêm mới thành công</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                // Trả kết quả về Client
                var res = _service.InsertEmployeeService(employee);

                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa giáo viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>1: Nếu sửa thành công</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            try
            {
                // Trả kết quả về Client
                var res = _service.UpdateEmployeeService(employee);

                return Ok(res);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xoá giáo viên
        /// </summary>
        /// <param name="id">ID của giáo viên cần xoá</param>
        /// <returns>1: Nếu xoá thành công</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                // Trả kết quả về Client
                var res = _repository.Delete(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xử lý Exception
        /// </summary>
        /// <param name="ex">Đối tượng Exception</param>
        /// <returns>400: Lỗi dữ liệu, 500: Lỗi hệ thống</returns>
        private IActionResult HandleException(Exception ex)
        {
            if (ex is MISAValidateExceptions)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    userMsg = ex.Message
                };
                return StatusCode(400, res);
            }
            else
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được hỗ trợ."
                };
                return StatusCode(500, res);
            }
        }
    }
}
