using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.TechnicianSpecifications
{
    public class TechnicianCompletedEmergencyRequestSpec:Specification<EmergencyRequest,int>
    {
        public TechnicianCompletedEmergencyRequestSpec(int techId,bool isCompleted) : base(req=>req.TechnicianId==techId&&req.IsCompleted==isCompleted) {
            AddInclude(req => req.category);
            AddInclude(req => req.CarOwner.ApplicationUser);
            AddInclude(req => req.CarOwner);
            
        }
    }
}
