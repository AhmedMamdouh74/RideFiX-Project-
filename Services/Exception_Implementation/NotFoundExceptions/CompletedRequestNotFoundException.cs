using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class CompletedRequestNotFoundException:Exception
    {
        public CompletedRequestNotFoundException(string message) : base(message) { 
        }
    }
}
