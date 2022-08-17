namespace MISA.Web06.Core.Models
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// ID phòng
        /// </summary>
        public Guid DepartmentID { get; set; }
        /// <summary>
        /// Tên phòng
        /// </summary>
        public string? DepartmentName { get; set; }
        
    }
}
