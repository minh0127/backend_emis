namespace MISA.Web06.Common.Models
{
    public class BaseEntity
    {
        // Ngày tạo bản ghi
        public DateTime? CreatedDate { get; set; }
        // Người tạo bản ghi
        public string? CreatedBy { get; set; }
        // Ngày sửa bản ghi
        public DateTime? ModifiedDate { get; set; }
        // Người sửa bản ghi
        public string? ModifiedBy { get; set; }
    }
}
