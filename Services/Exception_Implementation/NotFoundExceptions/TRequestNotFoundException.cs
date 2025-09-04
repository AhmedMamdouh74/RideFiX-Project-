using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class TRequestNotFoundException:Exception
    {
        public TRequestNotFoundException(string message) : base(message)
        {
        }
    }
}
