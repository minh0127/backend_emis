using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Models
{
    public class EmployeeSubject : BaseEntity
    {
        /// <summary>
        /// ID bảng trung gian giáo viên và môn học
        /// </summary>
        public Guid EmployeeSubjectID { get; set; }

        /// <summary>
        /// ID môn học
        /// </summary>
        public Guid SubjectID { get; set; }

        /// <summary>
        /// ID cán bộ giáo viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        public EmployeeSubject(Guid employeeID,Guid subjectID)
        {
            EmployeeID = employeeID;
            SubjectID = subjectID;
        }
    }
}
