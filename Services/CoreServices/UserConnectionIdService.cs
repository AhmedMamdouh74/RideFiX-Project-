using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Contracts.ReposatoriesContract;
using Domain.Contracts.SpecificationContracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ConnectionDtos;

namespace Service.CoreServices
{
    public class UserConnectionIdService : IUserConnectionIdService
    {
        IUnitOfWork UnitOfWork { get; set; }
        private readonly IMapper _mapper;

        public UserConnectionIdService(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            this.UnitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(UserConnectionIdDto dto)
        {
            var entity = _mapper.Map<UserConnectionIds>(dto);
            await UnitOfWork.ConnectionIdsRepository.AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<UserConnectionIdDto?> GetByIdsAsync(UserConnectionIdDto keyDto)
        {
            var entity = await UnitOfWork.ConnectionIdsRepository.GetByIdsAsync(keyDto.ConnectionId, keyDto.ApplicationUserId);
            return entity is null ? null : _mapper.Map<UserConnectionIdDto>(entity);
        }

        public async Task<UserConnectionIdDto?> GetBySpecIdAsync(IUserConnectionIdsSpecification spec)
        {
            var entity = await UnitOfWork.ConnectionIdsRepository.GetBySpecIdAsync(spec);
            return entity is null ? null : _mapper.Map<UserConnectionIdDto>(entity);
        }

        public async Task DeleteAsync(UserConnectionIdDto keyDto)
        {
            await UnitOfWork.ConnectionIdsRepository.DeleteAsync(keyDto.ConnectionId, keyDto.ApplicationUserId);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserConnectionIdDto>> GetAllAsync()
        {
            var list = await UnitOfWork.ConnectionIdsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserConnectionIdDto>>(list);
        }

        public async Task<IEnumerable<UserConnectionIdDto>> GetAllAsync(IUserConnectionIdsSpecification spec)
        {
            var list = await UnitOfWork.ConnectionIdsRepository.GetAllAsync(spec);
            return _mapper.Map<IEnumerable<UserConnectionIdDto>>(list);
        }

        public async Task UpdateAsync(UserConnectionIdDto dto)
        {
            var entity = _mapper.Map<UserConnectionIds>(dto);
            await UnitOfWork.ConnectionIdsRepository.UpdateAsync(entity);
            await UnitOfWork.SaveChangesAsync();
        }
    }

}
