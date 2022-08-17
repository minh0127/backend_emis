using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        /// <summary>
        /// Thêm mới service
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>1: Nếu thêm mới thành công</returns>
        /// CreatedBy: MINHBUI(04/08/2022)
        int InsertService(MISAEntity entity);

        /// <summary>
        /// Cập nhật service
        /// </summary>
        /// <param name="entity">Đối tượng thực thi</param>
        /// <returns>1: Nếu sửa thành công</returns>
        /// CreatedBy: MINHBUI(04/08/2022)
        int UpdateService(MISAEntity entity);
    }
}
