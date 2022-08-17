namespace MISA.Web06.Common.Models
{
    public class Employee : BaseEntity
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? FactoryID { get; set; }
        public Guid? SubjectID { get; set; }
        public Guid? DepartmentID { get; set; }
        public int? Training { get; set; }
        public int? WorkStatus { get; set; }
        public DateTime? OutWorkDate { get; set; }

    }
}
