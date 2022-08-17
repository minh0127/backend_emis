using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Models
{
    public class EmployeeDepartment : BaseEntity
    {
        /// <summary>
        /// ID bảng trung gian giáo viên và phòng kho
        /// </summary>
        public Guid EmployeeDepartmentID { get; set; }

        /// <summary>
        /// ID phòng kho
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// ID cán bộ giáo viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        public EmployeeDepartment(Guid employeeID, Guid departmentID)
        {
            EmployeeID = employeeID;
            DepartmentID = departmentID;
        }
    }
}
