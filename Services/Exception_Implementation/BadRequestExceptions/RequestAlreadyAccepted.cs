using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.BadRequestExceptions
{
    public class RequestAlreadyAccepted : BadRequestException
    {
        public RequestAlreadyAccepted() : base("Request Already Accepted Or rejected")
        {
        }
    }
}
