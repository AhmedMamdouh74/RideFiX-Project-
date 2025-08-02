using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification_Implementation.RequestSpecifications
{
    public abstract class  RequestSpecification : IRequestSpecification
    {
        #region Includes
        public Expression<Func<EmergencyRequestTechnicians, bool>> Criteria { get; private set; }
        public RequestSpecification(Expression<Func<EmergencyRequestTechnicians, bool>> _criteria)
        {
            Criteria = _criteria;
        }


        public List<Expression<Func<EmergencyRequestTechnicians, object>>> Includes { get; } = [];


        public void AddInclude(Expression<Func<EmergencyRequestTechnicians, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        #endregion

        #region Ordering
        public Expression<Func<EmergencyRequestTechnicians, object>> OrderBy { get; private set; }

        public Expression<Func<EmergencyRequestTechnicians, object>> OrderByDescending { get; private set; }

        public void SetOrderBy(Expression<Func<EmergencyRequestTechnicians, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void SetOrderByDescending(Expression<Func<EmergencyRequestTechnicians, object>> orderByDescendingExpression)
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
