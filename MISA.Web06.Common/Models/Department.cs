namespace MISA.Web06.Common.Models
{
    public class Department : BaseEntity
    {
        // ID phòng
        public Guid DepartmentID { get; set; }
        // Tên phòng
        public string? DepartmentName { get; set; }
        
    }
}
