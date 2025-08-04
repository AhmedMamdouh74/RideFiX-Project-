using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.CarOwnerSpecifications
{
    public class CarOwnerChatSpecification : Specification<ChatSession, int>
    {
        public CarOwnerChatSpecification(int Userid) : base(s => s.CarOwnerId == Userid && s.IsClosed == true)
        {
            AddInclude(s => s.CarOwner);
            AddInclude(s => s.massages );
            AddInclude(s => s.CarOwner.ApplicationUser);


            
        }
        public static IQueryable<ChatSession> ApplyMessageSorting(IQueryable<ChatSession> query)
        {
            return query
                .OrderByDescending(s => s.StartAt)
                .ThenByDescending(s => s.massages.Max(m => m.SentAt));
        }
    }
}
