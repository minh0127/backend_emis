namespace MISA.Web06.Core.Models
{
    public class Subject : BaseEntity
    {
        /// <summary>
        /// ID môn học
        /// </summary>
        public Guid SubjectID { get; set; }
        /// <summary>
        /// Tên môn học
        /// </summary>
        public string? SubjectName { get; set; }
    }
}
