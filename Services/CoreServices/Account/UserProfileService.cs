using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions.Account;
using SharedData.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.Account
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserProfileService(IUnitOfWork _unitOfWork,IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<ReadUserDetailsDTO> GetTechnicianDetailsAsync(int TechnicianId)
        {
            var techSpec = new TechnicianWithAppUserSpec(TechnicianId);
            var technician = await unitOfWork.GetRepository<Technician, int>().GetByIdAsync(techSpec);
            if (technician == null) return null;
            return mapper.Map<ReadUserDetailsDTO>(technician);
        }
    }
}
