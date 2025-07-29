using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;

namespace Domain.Contracts.SpecificationContracts
{
    public class IUserConnectionIdsSpecification
    {
        public Expression<Func<UserConnectionIds, bool>> Criteria { get; }
        public List<Expression<Func<UserConnectionIds, object>>> Includes { get; }
        public Expression<Func<UserConnectionIds, object>> OrderBy { get; }
        public Expression<Func<UserConnectionIds, object>> OrderByDescending { get; }
        public int? Take { get; }
        public int Skip { get; }
        public bool IsPagingEnabled { get; set; }
    }
}
