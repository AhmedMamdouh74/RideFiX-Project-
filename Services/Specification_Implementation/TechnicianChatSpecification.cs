using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation
{
    public class TechnicianChatSpecification : Specification<ChatSession, int>
    {
        public TechnicianChatSpecification(int userId) : base(s => s.TechnicianId == userId)
        {
            AddInclude(t => t.Technician);
            AddInclude(t => t.massages);
            AddInclude(s => s.Technician.ApplicationUser);
        }

    }
}
