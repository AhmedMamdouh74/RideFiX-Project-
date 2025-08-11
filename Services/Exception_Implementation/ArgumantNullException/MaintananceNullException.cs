using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exception_Implementation.ArgumantNullException
{
     public class MaintananceNullException : ArgumentNullException
    {
        public MaintananceNullException() : base("Maintenance type or last maintenance date cannot be null or empty.")
        {
        }
  
      
    }
}
