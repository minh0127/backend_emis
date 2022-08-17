namespace MISA.Web06.Core.Models
{
    public class Employee : BaseEntity
    {
        /// <summary>
        /// ID của giáo viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Số hiệu cán bộ
        /// </summary>
        [MISARequired]
        [PropNameDisplay("Số hiệu cán bộ")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [MISARequired]
        [PropNameDisplay("Họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// true: Đã đào tạo; false: Chưa đào tạo
        /// </summary>
        public int? Training { get; set; }

        /// <summary>
        /// true: Đang làm việc; false: Đã nghỉ việc
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// Ngày nghỉ việc
        /// </summary>
        public DateTime? OutWorkDate { get; set; }

        /// <summary>
        /// ID của tổ bộ môn
        /// </summary>
        public Guid? FactoryID { get; set; }

        /// <summary>
        /// Tên tổ bộ môn
        /// </summary>
        public string? FactoryName { get; set; }

        /// <summary>
        /// ID môn
        /// </summary>
        public string? SubjectID { get; set; }

        /// <summary>
        /// Tên môn
        /// </summary>
        public string? SubjectName { get; set; }

        /// <summary>
        /// ID phòng kho
        /// </summary>
        public string? DepartmentID { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
    }
}
