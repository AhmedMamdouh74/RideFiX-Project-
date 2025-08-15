using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class MaintananceNotFoundException : NotFoundException
    {
        public MaintananceNotFoundException() : base("Maintenance type not found.")
        {
        }

    }
}
