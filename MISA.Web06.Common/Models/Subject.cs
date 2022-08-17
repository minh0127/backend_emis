namespace MISA.Web06.Common.Models
{
    public class Subject : BaseEntity
    {
        // ID môn học
        public Guid SubjectID { get; set; }
        // Tên môn học
        public string? SubjectName { get; set; }
    }
}
