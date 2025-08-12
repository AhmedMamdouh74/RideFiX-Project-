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
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Specification_Implementation.ConnectionIdsSpecification;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ConnectionDtos;

namespace Service.CoreServices.ChatServices
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
        public async Task DeleteAsync(UserConnectionIdDto keyDto)
        {
            await UnitOfWork.ConnectionIdsRepository.DeleteAsync(keyDto.ConnectionId, keyDto.ApplicationUserId);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<UserConnectionIdDto>> SearchByCarOwnerId(int EntityId)
        {
            var Repo = UnitOfWork.GetRepository<CarOwner, int>();
            var owner = await Repo.GetByIdAsync(EntityId);
            if (owner == null)
            {
                throw new ArgumentException("Notfound");
            }
            List<UserConnectionIdDto> users = await GetByUserId(owner.ApplicationUserId);
            return users;
        }

        public async Task<List<UserConnectionIdDto>> SearchByTechnichanId(int EntityId)
        {
            var Repo = UnitOfWork.GetRepository<Technician, int>();
            var owner = await Repo.GetByIdAsync(EntityId);
            if (owner == null)
            {
                throw new ArgumentException("Notfound");
            }
            List<UserConnectionIdDto> users = await GetByUserId(owner.ApplicationUserId);
            return users;
        }

        private async Task<List<UserConnectionIdDto>> GetByUserId(string _userId)
        {
            var spec = new SearchByUserIdSpecification(_userId);
            var userConn = await UnitOfWork.ConnectionIdsRepository.GetAllAsync(spec);
            List<UserConnectionIdDto> users = new List<UserConnectionIdDto>();
            foreach (var item in userConn)
            {
                users.Add(new UserConnectionIdDto()
                {
                    ApplicationUserId = item.ApplicationUserId,
                    ConnectionId = item.ConnectionId,
                });
            }
            return users;
        }
    }
}
