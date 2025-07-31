using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class RequestDetailsSpecification : Specification<EmergencyRequest, int>
    {
        public RequestDetailsSpecification(int requestId)
            : base(r => r.Id == requestId)
        {
            AddInclude(r => r.category);
            AddInclude(r => r.Technician);
            AddInclude(r => r.Technician.ApplicationUser);
            AddInclude(r => r.EmergencyRequestTechnicians);

        }
    }
    
}
