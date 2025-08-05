using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.ArgumantNullException
{
    public class ReviewNullException : ArgumentNullException
    {
        public ReviewNullException() : base("يرجي اضافه تقييم !!")
        {
        }
    }
}
