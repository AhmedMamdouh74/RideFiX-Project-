using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.ArgumantNullException
{
    public class ShoppingCartArgumentException : ArgumentException
    {
        public ShoppingCartArgumentException(string message) : base(message)
        {
        }
       
    }
}
