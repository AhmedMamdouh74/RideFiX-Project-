using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;

//using Domain.Contracts.SpecificationContracts;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<T,TK> where T : BaseEntity<TK>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TK id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(TK id);
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T, TK> specification);
        Task<T> GetByIdAsync(ISpecification<T, TK> specification);
        Task<int> CountAsync(ISpecification<T, TK> specification);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T, TK> spec);

        public IQueryable<T> GetAllQueryable(ISpecification<T, TK> spec);

    }
}
