using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public static class EmergencySpecificationEvaluation
    {
        public static IQueryable<EmergencyRequestTechnicians> ApplySpecification
            (this IQueryable<EmergencyRequestTechnicians> query, IRequestSpecification specification) 
        {
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take ?? 10); 
            }
            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
