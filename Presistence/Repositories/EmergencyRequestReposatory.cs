using Domain.Contracts.ReposatoriesContract;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class EmergencyRequestReposatory : IEmergencyRequestReposatory
    {
        private readonly ApplicationDbContext _context;

        public EmergencyRequestReposatory(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmergencyRequestTechnicians entity)
        {
            await _context.EmergencyRequestTechnicians.AddAsync(entity);
        }

        public async Task<IEnumerable<EmergencyRequestTechnicians>> GetAllAsync()
        {
            return await _context.EmergencyRequestTechnicians.ToListAsync();
        }

        public async Task<IEnumerable<EmergencyRequestTechnicians>> GetAllAsync(IRequestSpecification spec)
        {
            var query = _context.EmergencyRequestTechnicians.AsQueryable();
            query = query.ApplySpecification(spec);
            return await query.ToListAsync();
        }

        public async Task<EmergencyRequestTechnicians?> GetByIdsAsync(int technicianId, int emergencyRequestId)
        {
            return await _context.EmergencyRequestTechnicians
                .FirstOrDefaultAsync(e => e.TechnicianId == technicianId && e.EmergencyRequestId == emergencyRequestId);
        }

        public async Task<EmergencyRequestTechnicians?> GetBySpecIdAsync(IRequestSpecification spec)
        {
            var query = _context.EmergencyRequestTechnicians.AsQueryable();
            query = query.ApplySpecification(spec);
            return await query.FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int technicianId, int emergencyRequestId)
        {
            var entity = await GetByIdsAsync(technicianId, emergencyRequestId);
            if (entity != null)
            {
                _context.EmergencyRequestTechnicians.Remove(entity);
            }
        }

        public Task UpdateAsync(EmergencyRequestTechnicians entity)
        {
            _context.EmergencyRequestTechnicians.Update(entity);
            return Task.CompletedTask;
        }
     

    }
}