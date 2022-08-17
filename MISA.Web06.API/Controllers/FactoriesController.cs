using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web06.Core.Exceptions;
using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Interfaces.Services;

namespace MISA.Web06.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FactoriesController : ControllerBase
    {
        IFactoryService _service;
        IFactoryRepository _repository;
        public FactoriesController(IFactoryService service, IFactoryRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu tổ bộ môn
        /// </summary>
        /// <returns>Danh sách tổ bộ môn</returns>
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
        /// Lấy dữ liệu tổ bộ môn theo ID
        /// </summary>
        /// <param name="id">ID tổ bộ môn cần lấy dữ liệu</param>
        /// <returns>Dữ liệu tổ bộ môn có ID = id</returns>
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
