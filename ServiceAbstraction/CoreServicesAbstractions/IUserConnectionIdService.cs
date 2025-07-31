using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.SpecificationContracts;
using SharedData.DTOs.ConnectionDtos;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IUserConnectionIdService
    {
        Task AddAsync(UserConnectionIdDto dto);
        Task<UserConnectionIdDto?> GetByIdsAsync(UserConnectionIdDto keyDto);
        Task<UserConnectionIdDto?> GetBySpecIdAsync(IUserConnectionIdsSpecification spec);
        Task DeleteAsync(UserConnectionIdDto keyDto);
        Task<IEnumerable<UserConnectionIdDto>> GetAllAsync();
        Task<IEnumerable<UserConnectionIdDto>> GetAllAsync(IUserConnectionIdsSpecification spec);
        Task UpdateAsync(UserConnectionIdDto dto);
    }
}
