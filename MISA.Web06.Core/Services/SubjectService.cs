using MISA.Web06.Core.Interfaces.Repository;
using MISA.Web06.Core.Interfaces.Services;
using MISA.Web06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Services
{
    public class SubjectService : BaseService<Subject>, ISubjectService
    {
        public SubjectService(IBaseRepository<Subject> repository) : base(repository)
        {
        }
    }
}
