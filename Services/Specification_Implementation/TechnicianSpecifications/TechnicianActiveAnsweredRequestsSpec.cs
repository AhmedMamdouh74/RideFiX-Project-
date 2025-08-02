using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.TechnicianSpecifications
{
    public class TechnicianActiveAnsweredRequestsSpec : Specification<EmergencyRequest, int>
    {
        public TechnicianActiveAnsweredRequestsSpec(int technicianId) : base(req =>
            !req.IsCompleted &&
            req.EmergencyRequestTechnicians.Any(link =>
                link.TechnicianId == technicianId &&
                link.CallStatus == RequestState.Answered))
        { }
    }
}
