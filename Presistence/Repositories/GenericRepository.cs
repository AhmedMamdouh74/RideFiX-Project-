using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Services.Specification_Implementation;
//using Service.Specification_Implementation;

namespace Presistence.Repositories
{
    public class GenericRepository<T, TK> : IGenericRepository<T, TK> where T : BaseEntity<TK>
    {
        #region Old Repository Code
        readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(TK id)
        {
            var element = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
            if (element != null)
            {
                _context.Set<T>().Remove(element);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TK id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
            if (entity == null)
            {
                return null!;
            }
            return entity;
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        #endregion

        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T, TK> specification)
        {
            var query = _context.Set<T>().AsQueryable();
            query = SpecificationEvaluation.ApplySpecification(query, specification);
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(ISpecification<T, TK> specification)
        {
            var query = _context.Set<T>().AsQueryable();
            query = SpecificationEvaluation.ApplySpecification(query, specification);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<T, TK> specification)
        {
            var query = _context.Set<T>().AsQueryable();
            query = SpecificationEvaluation.ApplySpecification(query, specification);
            return await query.CountAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T, TK> spec)
        {
            var query = _context.Set<T>().AsQueryable();
            query = SpecificationEvaluation.ApplySpecification(query, spec);
            return await query.ToListAsync();
        }
    }
}
