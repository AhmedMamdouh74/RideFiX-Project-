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

        Task DeleteAsync(UserConnectionIdDto keyDto);

        public  Task<List<UserConnectionIdDto>> SearchByTechnichanId(int EntityId);
        public Task<List<UserConnectionIdDto>> SearchByCarOwnerId(int EntityId);

    }
}
