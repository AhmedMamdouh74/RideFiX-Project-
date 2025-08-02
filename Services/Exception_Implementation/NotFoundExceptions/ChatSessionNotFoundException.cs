using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class ChatSessionNotFoundException : NotFoundException
    {
        public ChatSessionNotFoundException()
            : base($"Chat session not found for this request.")
        {
        }
    }
}
