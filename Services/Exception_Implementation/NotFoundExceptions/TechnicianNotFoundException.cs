using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class TechnicianNotFoundException : NotFoundException
    {
        public TechnicianNotFoundException() : base("لا يوجد فني تقدم علي هذه الخدمه!!")
        {
        }
    }
}
