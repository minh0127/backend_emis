using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Interfaces.Repository
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu của bảng
        /// </summary>
        /// <returns>Danh sách các đối tượng</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        public IEnumerable<MISAEntity> Get();

        /// <summary>
        /// Lấy dữ liệu của bảng theo ID
        /// </summary>
        /// <param name="id">Khoá chính của đối tượng</param>
        /// <returns>Đối tượng có ID = id</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        public MISAEntity GetById(Guid id);

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng được thêm vào bảng</param>
        /// <returns>Số đối tượng được thêm mới</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        public int Insert(MISAEntity entity);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng được cập nhật</param>
        /// <returns>1: Nếu cập nhật thành công đối tượng</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        public int Update(MISAEntity entity);

        /// <summary>
        /// Xoá đối tượng
        /// </summary>
        /// <param name="id">Khoá chính của đối tượng</param>
        /// <returns>1: Nếu xoá thành công đối tượng</returns>
        /// CreatedBy: MINHBUI (04/08/2022)
        public int Delete(Guid id);
    }
}
