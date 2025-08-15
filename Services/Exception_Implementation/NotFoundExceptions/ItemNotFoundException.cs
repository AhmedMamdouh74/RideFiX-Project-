using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class ItemNotFoundException : NotFoundException
    {
        public ItemNotFoundException(string message) : base(message)
        {
        }
       
    }
    
}
