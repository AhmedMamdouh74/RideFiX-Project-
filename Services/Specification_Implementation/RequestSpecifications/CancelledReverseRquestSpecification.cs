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
    public class CancelledReverseRquestSpecification : Specification<TechReverseRequest, int>
    {
        public CancelledReverseRquestSpecification(int requestId) :
            base(s => s.EmergencyRequestId == requestId
                &&( s.CallState != RequestState.Cancelled || s.CallState != RequestState.Rejected))
        {
        }
    }
}
