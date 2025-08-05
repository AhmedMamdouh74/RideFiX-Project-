using Domain.Contracts.SpecificationContracts;
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
    public class ChatSessionAllSpecification : Specification<ChatSession, int>
    {
        public ChatSessionAllSpecification(int technicianId, int CarOwnerId) : 
            base(s => s.TechnicianId == technicianId 
            && s.CarOwnerId == CarOwnerId)
        {
        }
    }
}
