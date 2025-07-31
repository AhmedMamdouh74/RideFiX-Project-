using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;

namespace Service.Specification_Implementation
{
    internal class NotCompletedRequestSpecification : Specification<EmergencyRequest, int>
    {
        public NotCompletedRequestSpecification(int Id) : base(x => x.CarOwnerId == Id && x.IsCompleted == false)
        {
            AddInclude(x => x.EmergencyRequestTechnicians);
            AddInclude(x => x.TechReverseRequests);
            AddInclude(x => x.category);
            AddInclude(x => x.Technician);
            AddInclude(x => x.Technician.ApplicationUser);
        }
    }
}
