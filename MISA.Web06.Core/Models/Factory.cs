namespace MISA.Web06.Core.Models
{
    public class Factory : BaseEntity
    {
        /// <summary>
        /// ID tổ bộ môn
        /// </summary>
        public Guid FactoryID { get; set; }
        /// <summary>
        /// Tên tổ bộ môn
        /// </summary>
        public string? FactoryName { get; set; }
    }
}
