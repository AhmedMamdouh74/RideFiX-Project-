using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.ArgumantNullException
{
    public class ProductArgumentException : ArgumentNullException
    {
        public ProductArgumentException(string message) : base(message)
        {
        }
        
    }
}
