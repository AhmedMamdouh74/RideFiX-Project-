using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.ChatSessionsSpecifications
{
    public class GetChatSessionByTechnicianId : Specification<ChatSession, int>
    {
        public GetChatSessionByTechnicianId(int TechID) : base(s => s.TechnicianId == TechID && s.IsClosed == false)
        {
            AddInclude(s => s.massages.OrderBy(s => s.SentAt));
            AddInclude(s => s.Technician);
            AddInclude(s => s.CarOwner);
        }
    }
}
