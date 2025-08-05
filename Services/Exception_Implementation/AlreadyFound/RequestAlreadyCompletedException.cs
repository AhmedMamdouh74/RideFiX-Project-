using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.AlreadyFound
{
    public class RequestAlreadyCompletedException : Exception
    {
        public RequestAlreadyCompletedException() : base("هذا الطلب بالفعل مكتمل")
        {
        }

    }
}
