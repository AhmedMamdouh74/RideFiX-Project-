using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Microsoft.AspNetCore.Http;
using ServiceAbstraction.CoreServicesAbstractions.CarMservices;
using SharedData.DTOs.CarMaintananceDTOs;
using SharedData.DTOs.MTypesDtos;

namespace Service.CoreServices.CarMservices
{
    public class MaintenanceTypesService : IMaintenanceTypesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MaintenanceTypesService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ICarServices carServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<MaintenanceTypeDetailsDto> GetMTypebyIdAsync(int Id)
        {
            var Repo = unitOfWork.GetRepository<MaintenanceTypes , int>();
            var MType = mapper.Map<MaintenanceTypeDetailsDto>(await Repo.GetByIdAsync(Id));
            return MType;
        }

        public async Task<List<MaintenanceTypeDTO>> GetAll()
        {
            var Repo = unitOfWork.GetRepository<MaintenanceTypes, int>();
            var MType = mapper.Map<List<MaintenanceTypeDTO>>(await Repo.GetAllAsync());
            return MType;
        }
    }
}
