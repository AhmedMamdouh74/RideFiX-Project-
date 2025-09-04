using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.TechnicianSpecifications
{
    public class TechnicianChatSpecification : Specification<ChatSession, int>
    {
        public TechnicianChatSpecification(int UserId) : base(s => s.TechnicianId == UserId && s.IsClosed == true)
        {
            AddInclude(t => t.massages);
            AddInclude(t => t.CarOwner);
            AddInclude(t => t.CarOwner.ApplicationUser);

        }
        public static IQueryable<ChatSession> ApplyMessageSorting(IQueryable<ChatSession> query)
        {
            return query
                .OrderByDescending(s => s.StartAt)
                .ThenByDescending(s => s.massages.Max(m => m.SentAt));
        }

    }
}
