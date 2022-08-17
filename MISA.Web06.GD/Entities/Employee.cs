namespace MISA.Web06.GD.Entities
{
    public class Employee : BaseEntity
    {
        public Guid EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public int MyProperty { get; set; }
    }
}
