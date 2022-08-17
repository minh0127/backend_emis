using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web06.Core.Exceptions
{
    public class MISAValidateExceptions : Exception
    {

        public string? ErrorMsg { get; set; }
        public IDictionary Errors { get; set; }

        public MISAValidateExceptions(string errorMsg)
        {
            ErrorMsg = errorMsg;
        }

        public MISAValidateExceptions(List<string> errorMsg)
        {
            Errors = new Dictionary<string, object>();
            Errors.Add("error", errorMsg);
        }

        public override string Message => this.ErrorMsg;
        public override IDictionary Data => Errors;
    }
}
