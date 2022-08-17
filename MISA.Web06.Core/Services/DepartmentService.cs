using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web06.Core.Models;
using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Interfaces.Services;
using MISA.Web06.Core.Exceptions;

namespace MISA.Web06.Core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        IDepartmentRepository _repository;
        public DepartmentService(IDepartmentRepository repository):base(repository)
        {
            _repository = repository;
        }

        public int InsertService(Department department)
        {
            var isValid = true;
            // Validate dữ liệu
            // Tên phòng ban không được phép để trống
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                isValid = false;
                var res = new
                {
                    devMsg = "Dữ liệu đầu vào không hợp lệ",
                    userMsg = "Tên phòng ban không được phép để trống."
                };
                throw new MISAValidateExceptions(res.userMsg);
            }

            // Dữ liệu hợp lệ thì gọi yêu cầu thêm mới vào database
            if (isValid)
            {
                var res = _repository.Insert(department);
                return res;
            }
            else
            {
                throw new MISAValidateExceptions("Dữ liệu đầu vào không hợp lệ");
            }


        }

        public int UpdateService(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
