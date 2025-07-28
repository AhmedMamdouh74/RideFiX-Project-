using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.AlreadyFound
{
    public class RequestAlreadyFoundException : Exception
    {
        public RequestAlreadyFoundException() : base("جاري تنفيذ طلبك بالفعل")
        {
        }
    }
}
