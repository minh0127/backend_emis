using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web06.Core.Models;
using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Interfaces.Services;
using MISA.Web06.Core.Exceptions;

namespace MISA.Web06.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IDepartmentService _service;
        IDepartmentRepository _repository;
        public DepartmentsController(IDepartmentService service, IDepartmentRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu phòng
        /// </summary>
        /// <returns>Danh sách phòng</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Trả về dữ liệu cho client
                var data = _repository.Get();

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy dữ liệu phòng theo ID
        /// </summary>
        /// <param name="id">ID phòng cần lấy dữ liệu</param>
        /// <returns>Dữ liệu phòng có ID = id</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            try
            {
                // Trả về dữ liệu cho client
                var data = _repository.GetById(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Ghi log vào hệ thống
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới phòng
        /// </summary>
        /// <param name="department"></param>
        /// <returns>1: Nếu thêm mới thành công</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        [HttpPost]
        public IActionResult Post(Department department)
        {
            try
            {
                // Trả kết quả về Client
                var res = _service.InsertService(department);

                return StatusCode(201, res);
            }
            catch(Exception ex)
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
            var res = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi xảy ra, vui lòng liên hệ MISA để được hỗ trợ."
            };
            if (ex is MISAValidateExceptions)
                return StatusCode(400, res);
            return StatusCode(500, res);
        }
    }
}
