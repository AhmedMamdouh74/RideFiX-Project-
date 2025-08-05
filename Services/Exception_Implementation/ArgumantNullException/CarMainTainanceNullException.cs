using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.ArgumantNullException
{
    public class CarMainTainanceNullException : ArgumentNullException
    {
        public CarMainTainanceNullException() : base("Car Maintenance Record cannot be null")
        {
        }
       
    }
}
