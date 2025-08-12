using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.ChatSessionsSpecifications
{
    public class GetChatSessionByCarOwnerId : Specification<ChatSession, int>
    {
        public GetChatSessionByCarOwnerId(int carOwnerId)
            : base(s => s.CarOwnerId == carOwnerId && s.IsClosed == false)
        {
            AddInclude(s => s.massages.OrderBy(s => s.SentAt));

        }
    }
    
}
