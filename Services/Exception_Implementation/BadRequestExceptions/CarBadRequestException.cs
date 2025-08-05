using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.BadRequestExceptions
{
    public class CarBadRequestException:Exception
    {
        public CarBadRequestException() : base("Invalid input Data") { }

    }
}
