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
    public class RequestBreifSpecification : Specification<EmergencyRequest, int>
    {
        public RequestBreifSpecification(int CarOwnerID)
            : base(r => r.CarOwnerId == CarOwnerID 
            && r.IsCompleted == true)
        {
            AddInclude(r => r.category);
            AddInclude(r => r.Technician);
            AddInclude(r => r.Technician.ApplicationUser);

        }
    }
    
}
