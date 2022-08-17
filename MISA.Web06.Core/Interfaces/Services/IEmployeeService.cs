using MISA.Web06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        public int InsertEmployeeService(Employee employee);
        public int UpdateEmployeeService(Employee employee);
    }
}
