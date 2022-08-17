using MISA.Web06.Core.Exceptions;
using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Interfaces.Services;
using MISA.Web06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository repository):base(repository)
        {
            _repository = repository;
        }

        public int InsertEmployeeService(Employee employee)
        {
            // Validate
            isValid = Validate(employee);
            isValidCustom = ValidateCustom(employee);
            // Thực hiện thêm mới
            if (isValid && isValidCustom)
            {
                var res = _repository.InsertEmployee(employee);
                return res;
            }
            else
            {
                throw new MISAValidateExceptions(ErrorMsg);
            }
        }

        public int UpdateEmployeeService(Employee employee)
        {
            // Validate
            isValid = Validate(employee);
            
            // Thực hiện thêm mới
            if (isValid && isValidCustom)
            {
                var res = _repository.UpdateEmployee(employee);
                return res;
            }
            else
            {
                throw new MISAValidateExceptions(ErrorMsg);
            }
        }

        protected override bool ValidateCustom(Employee entity)
        {
            // Check trùng số hiệu cán bộ
            if (_repository.CheckEmployeeCodeExist(entity.EmployeeCode))
            {
                isValidCustom = false;
                ErrorMsg.Add("Số hiệu cán bộ đã tồn tại trong hệ thống");
            }

            // Validate số điện thoại

            // Validate email

            return isValidCustom;
        }

        
    }
}
