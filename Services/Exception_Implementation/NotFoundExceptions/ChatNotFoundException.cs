using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class ChatNotFoundException : NotFoundException
    {

        public ChatNotFoundException() : base("Chat not found.")
        {
        }
       
    
    }
}
