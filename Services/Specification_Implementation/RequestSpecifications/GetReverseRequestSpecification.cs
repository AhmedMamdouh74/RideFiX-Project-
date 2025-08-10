using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public class GetReverseRequestSpecification : Specification<TechReverseRequest , int>
    {
        public GetReverseRequestSpecification(int requestId) : base(s => s.EmergencyRequestId == requestId)
        {
            AddInclude(s => s.Technician);
            AddInclude(s => s.Technician.ApplicationUser);
        }
    }
    
}
