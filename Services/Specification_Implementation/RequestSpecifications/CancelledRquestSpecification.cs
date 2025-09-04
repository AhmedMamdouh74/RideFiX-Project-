using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class CancelledRquestSpecification : RequestSpecification
    {
        public CancelledRquestSpecification(int CarOwnerID, int requestId) : 
            base(e => e.EmergencyRequests.CarOwnerId == CarOwnerID && e.EmergencyRequestId == requestId
            &&(e.CallStatus != RequestState.Completed || e.CallStatus != RequestState.Rejected)
            )
        {
            AddInclude(e => e.EmergencyRequests);    
        }
    }
}
