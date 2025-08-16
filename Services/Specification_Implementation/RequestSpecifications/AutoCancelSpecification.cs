using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class AutoCancelSpecification : RequestSpecification
    {
        public AutoCancelSpecification(int reqId) : base(x=> x.EmergencyRequestId == reqId)
        {
        }
    }
}
