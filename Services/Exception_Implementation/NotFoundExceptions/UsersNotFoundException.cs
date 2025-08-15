using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class UsersNotFoundException:Exception
    {
        public UsersNotFoundException(string message):base(message) { }
    }
}
