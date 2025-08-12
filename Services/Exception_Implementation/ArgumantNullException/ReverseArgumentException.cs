using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.ArgumantNullException
{
    public class ReverseArgumentException : ArgumentNullException
    {
        public ReverseArgumentException(string message) : base(message)
        {
        }
        public ReverseArgumentException() : base("Invalid request ID.")
        {
        }
    }
    
}
