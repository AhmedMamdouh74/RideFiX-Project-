using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class ActiveRequestsForTechnicianSpec : Specification<EmergencyRequest, int>
    {
        public ActiveRequestsForTechnicianSpec(int technicianId)
            : base(req =>
                !req.IsCompleted &&
                 req.EmergencyRequestTechnicians.Any(link => link.CallStatus == RequestState.Waiting) &&
                !req.EmergencyRequestTechnicians.Any(link => link.TechnicianId == technicianId))
        {
            AddInclude(req => req.CarOwner.ApplicationUser);
            AddInclude(req => req.category);
            AddInclude(req => req.EmergencyRequestTechnicians);
        }
    }
}
