using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;
using SharedData.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class EmergencyRequestTechnicanSpecefication : Specification<EmergencyRequestTechnicians, int>
    {
        public EmergencyRequestTechnicanSpecefication(RequestQueryData requestQueryData)
        : base(r =>
 (!requestQueryData.CallState.HasValue || r.CallStatus == requestQueryData.CallState) &&
 (!requestQueryData.TechnicainId.HasValue || r.TechnicianId == requestQueryData.TechnicainId) && 
 (!requestQueryData.IsCompleted.HasValue || r.EmergencyRequests.IsCompleted == requestQueryData.IsCompleted)


)

        {
            AddInclude(r => r.Technician);
            AddInclude(r => r.EmergencyRequests);
            AddInclude(r => r.EmergencyRequests.CarOwner.ApplicationUser);
            AddInclude(r => r.EmergencyRequests.category);

        }
        public EmergencyRequestTechnicanSpecefication(int technicianId, RequestState requestState) : base(req => req.TechnicianId != technicianId && req.CallStatus == requestState)
        {
            AddInclude(req => req.Technician);
            AddInclude(req => req.EmergencyRequests);
            AddInclude(req => req.EmergencyRequests.CarOwner.ApplicationUser);
            AddInclude(req => req.EmergencyRequests.category);

        }
    }
}
