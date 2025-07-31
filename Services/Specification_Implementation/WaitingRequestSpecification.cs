using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    
    internal class WaitingRequestSpecification : Specification<EmergencyRequest, int>
    {
        public WaitingRequestSpecification(int Id) : base(x => x.CarOwnerId == Id 
                        && x.IsCompleted == false
                        && x.EmergencyRequestTechnicians.Any(y => y.CallStatus == RequestState.Waiting))
        {
            AddInclude(x => x.EmergencyRequestTechnicians);
        }
    }
}
