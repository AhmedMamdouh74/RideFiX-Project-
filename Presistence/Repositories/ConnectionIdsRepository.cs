using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.ReposatoriesContract;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    internal class ConnectionIdsRepository : IConnectionIdsRepository
    {
        private readonly ApplicationDbContext _context;

        public ConnectionIdsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserConnectionIds entity)
        {
            await _context.UserConnectionIds.AddAsync(entity);
        }

        public async Task<IEnumerable<UserConnectionIds>> GetAllAsync()
        {
            return await _context.UserConnectionIds.ToListAsync();
        }

        public async Task<IEnumerable<UserConnectionIds>> GetAllAsync(IUserConnectionIdsSpecification spec)
        {
            var query = _context.UserConnectionIds.AsQueryable();
            query = query.ApplySpecification(spec);
            return await query.ToListAsync();
        }

        public async Task<UserConnectionIds?> GetByIdsAsync(string connectionId, string userId)
        {
            return await _context.UserConnectionIds
                .FirstOrDefaultAsync(e => e.ConnectionId == connectionId && e.ApplicationUserId == userId);
        }

        public async Task<UserConnectionIds?> GetBySpecIdAsync(IUserConnectionIdsSpecification spec)
        {
            var query = _context.UserConnectionIds.AsQueryable();
            query = query.ApplySpecification(spec);
            return await query.FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(string connectionId, string userId)
        {
            var entity = await GetByIdsAsync(connectionId, userId);
            if (entity != null)
            {
                _context.UserConnectionIds.Remove(entity);
            }
        }

        public Task UpdateAsync(UserConnectionIds entity)
        {
            _context.UserConnectionIds.Update(entity);
            return Task.CompletedTask;
        }
    }
}
