using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.NotFoundExceptions
{
    public class CategoriesNotFoundException:Exception
    {
        public CategoriesNotFoundException(string message):base(message) { }
    }
}
