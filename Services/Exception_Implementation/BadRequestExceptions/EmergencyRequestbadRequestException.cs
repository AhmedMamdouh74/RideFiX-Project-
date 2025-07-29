using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.BadRequestExceptions
{
    public class EmergencyRequestbadRequestException:Exception
    {
        public EmergencyRequestbadRequestException(string message):base(message) { }
    }
}
