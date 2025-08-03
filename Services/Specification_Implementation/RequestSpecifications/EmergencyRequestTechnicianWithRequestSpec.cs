using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class EmergencyRequestTechnicianWithRequestSpec : Specification<EmergencyRequestTechnicians, int>
    {
        public EmergencyRequestTechnicianWithRequestSpec(int requestId, int technicianId)
            : base(ert =>
                ert.EmergencyRequestId == requestId &&
                ert.TechnicianId == technicianId)
        {
            AddInclude(ert => ert.EmergencyRequests);
            AddInclude(ert => ert.EmergencyRequests.category);
            AddInclude(ert => ert.EmergencyRequests.CarOwner);
            AddInclude(ert => ert.EmergencyRequests.CarOwner.ApplicationUser);
        }
    }
}
