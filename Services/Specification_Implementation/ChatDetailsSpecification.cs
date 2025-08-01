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
    public class ChatDetailsSpecification : Specification<ChatSession, int>
    {
        public ChatDetailsSpecification(int chatSession) : base(s => s.Id == chatSession)
        {
            AddInclude(s => s.massages.OrderByDescending(m => m.SentAt));
            AddInclude(s => s.CarOwner.ApplicationUser);
            AddInclude(s => s.CarOwner);
        }
    }
}
