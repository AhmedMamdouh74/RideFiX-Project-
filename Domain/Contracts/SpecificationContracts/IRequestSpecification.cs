using Domain.Entities.CoreEntites.EmergencyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.SpecificationContracts
{
    public interface IRequestSpecification
    {
        public Expression<Func<EmergencyRequestTechnicians, bool>> Criteria { get; }
        public List<Expression<Func<EmergencyRequestTechnicians, object>>> Includes { get; }
        public Expression<Func<EmergencyRequestTechnicians, object>> OrderBy { get; }
        public Expression<Func<EmergencyRequestTechnicians, object>> OrderByDescending { get; }
        public int? Take { get; }
        public int Skip { get; }
        public bool IsPagingEnabled { get; set; }
    }
}
