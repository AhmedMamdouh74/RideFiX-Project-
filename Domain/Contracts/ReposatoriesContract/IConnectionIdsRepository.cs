using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;

namespace Domain.Contracts.ReposatoriesContract
{
    public interface IConnectionIdsRepository
    {
        Task AddAsync(UserConnectionIds entity);
        Task<UserConnectionIds?> GetByIdsAsync(string connectionId, string userId);
        Task<UserConnectionIds?> GetBySpecIdAsync(IUserConnectionIdsSpecification spec);
        Task DeleteAsync(string connectionId, string userId);
        Task<IEnumerable<UserConnectionIds>> GetAllAsync();
        Task<IEnumerable<UserConnectionIds>> GetAllAsync(IUserConnectionIdsSpecification spec);
        Task UpdateAsync(UserConnectionIds entity);
    }
}
