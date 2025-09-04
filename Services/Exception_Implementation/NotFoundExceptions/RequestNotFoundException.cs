using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class RequestNotFoundException : Exception
    {
        public RequestNotFoundException() : base("لا يوجد اي طلبات حاليه للالغاء!")
        {
        }

    }
}
