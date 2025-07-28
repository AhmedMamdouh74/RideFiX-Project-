using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class RequestDetailsException : BadRequestException
    {
        public RequestDetailsException() : base($"لا يوجد طلب مكتمل لعرض تفاصيله ")
        {
        }
    }
}

