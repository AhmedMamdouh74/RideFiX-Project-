using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class TechnicianWithAppUserSpec : Specification<Technician, int>
    {
        public TechnicianWithAppUserSpec(int technicianId, int pin) : base(t => t.Id == technicianId && t.ApplicationUser.PIN == pin)
        {
            AddInclude(t => t.ApplicationUser);
        }
        public TechnicianWithAppUserSpec(int technicianId) : base(t => t.Id == technicianId) {
            AddInclude(t => t.ApplicationUser);
            AddInclude(t => t.TCategories);

        }
    }
}
