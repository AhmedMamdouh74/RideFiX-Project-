using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class CarNotFoundException : NotFoundException
    {
        public CarNotFoundException() : base($"لا يوجد سيارة لهذا العميل")
        {
        }
    }
}
