using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Services.Specification_Implementation;
using SharedData.DTOs.RequestsDTOs;

namespace Service.Specification_Implementation.ConnectionIdsSpecification
{
    public class ConnectionIdSpecification : IUserConnectionIdsSpecification
    {
        #region Includes
        public Expression<Func<UserConnectionIds, bool>> Criteria { get; private set; }
        public ConnectionIdSpecification(Expression<Func<UserConnectionIds, bool>> _criteria)
        {
            Criteria = _criteria;
        }

        public List<Expression<Func<UserConnectionIds, object>>> Includes { get; } = [];


        public void AddInclude(Expression<Func<UserConnectionIds, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        #endregion

        #region Ordering
        public Expression<Func<UserConnectionIds, object>> OrderBy { get; private set; }

        public Expression<Func<UserConnectionIds, object>> OrderByDescending { get; private set; }

        public void SetOrderBy(Expression<Func<UserConnectionIds, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void SetOrderByDescending(Expression<Func<UserConnectionIds, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        #endregion

        #region Pagination
        public int? Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; set; }
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        #endregion
    }

}

