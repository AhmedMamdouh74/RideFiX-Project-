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
    public class CarOwnerChatSpecification : Specification<ChatSession, int>
    {
        public CarOwnerChatSpecification(int Userid) : base(s => s.CarOwnerId == Userid)
        {
            AddInclude(s => s.CarOwner);
            AddInclude(s => s.massages);
        }
    }
}
