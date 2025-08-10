using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.ArgumantNullException
{
    public class MaintananceArgumentException : ArgumentNullException
    {
        public MaintananceArgumentException(string message) : base(message)
        {
        }
        public MaintananceArgumentException() : base("Maintenance ID must be greater than zero.")
        { 
        }
    }
}
