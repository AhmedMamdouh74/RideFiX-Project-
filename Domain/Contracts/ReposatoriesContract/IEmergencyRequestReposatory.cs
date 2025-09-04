using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.ReposatoriesContract
{
    public interface IEmergencyRequestReposatory
    {
        Task AddAsync(EmergencyRequestTechnicians entity);
        Task<EmergencyRequestTechnicians?> GetByIdsAsync(int technicianId, int emergencyRequestId);
        Task<EmergencyRequestTechnicians?> GetBySpecIdAsync(IRequestSpecification spec);
        Task DeleteAsync(int technicianId, int emergencyRequestId);
        Task<IEnumerable<EmergencyRequestTechnicians>> GetAllAsync();
        Task<IEnumerable<EmergencyRequestTechnicians>> GetAllAsync(IRequestSpecification spec);
        Task UpdateAsync(EmergencyRequestTechnicians entity);


    }
}
