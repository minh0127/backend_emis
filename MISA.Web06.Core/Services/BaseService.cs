using MISA.Web06.Core.Exceptions;
using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _repository;
        protected List<String> ErrorMsg;
        protected bool isValid = true;
        protected bool isValidCustom = true;
        
        public BaseService(IBaseRepository<MISAEntity> repository)
        {
            _repository = repository;
            ErrorMsg = new List<string>();
        }

        /// <summary>
        /// Thêm service
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="MISAValidateExceptions"></exception>
        /// CreatedBy: MINHBUI (04/08/2022)
        public int InsertService(MISAEntity entity)
        {
            // Validate
            isValid = Validate(entity);
            isValidCustom = ValidateCustom(entity);
            // Thực hiện thêm mới
            if (isValid && isValidCustom)
            {
                var res = _repository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAValidateExceptions(ErrorMsg);
            }    
        }

        /// <summary>
        /// Sửa service
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        public int UpdateService(MISAEntity entity)
        {
            // Validate
            isValid = Validate(entity);
            // Thực hiện Sửa
            if (isValid)
            {
                var res = _repository.Update(entity);
                return res;
            }
            else
            {
                throw new MISAValidateExceptions(ErrorMsg);
            }
        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true: Hợp lệ; false: Dữ liệu không hợp lệ</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        protected bool Validate(MISAEntity entity)
        {
            // Validate chung
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var value = property.GetValue(entity);
                var misaRequired = property.IsDefined(typeof(MISARequired), false);
                // Nếu dữ liệu trống hoặc null
                if (misaRequired == true && (value == null || value.ToString() == String.Empty))
                {
                    isValid = false;
                    var propNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();
                    propName = (propNameDisplay as PropNameDisplay).PropName;
                    ErrorMsg.Add($"{propName} không được để trống");
                }
            }

            return isValid;
        }

        protected virtual bool ValidateCustom(MISAEntity entity)
        {
            return true;
        }
    }
}
