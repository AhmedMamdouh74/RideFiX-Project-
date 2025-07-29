using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    public static class UserConnectionIdsSpecificationEvaluation
    {
        public static IQueryable<UserConnectionIds> ApplySpecification
            (this IQueryable<UserConnectionIds> query, IUserConnectionIdsSpecification specification)
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
