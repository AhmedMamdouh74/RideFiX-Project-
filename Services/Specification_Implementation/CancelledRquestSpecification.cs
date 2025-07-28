using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class CancelledRquestSpecification : RequestSpecification
    {
        public CancelledRquestSpecification(int CarOwnerID) : base(e => e.EmergencyRequests.CarOwnerId == CarOwnerID)
        {
            AddInclude(e => e.EmergencyRequests);    
        }
    }
}
