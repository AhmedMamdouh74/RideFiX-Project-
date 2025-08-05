using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Microsoft.AspNetCore.Http;
using Service.Exception_Implementation.ArgumantNullException;
using ServiceAbstraction.CoreServicesAbstractions.CarMservices;
using SharedData.DTOs.CarMaintananceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.CarMservices
{
    public class CarMaintananceService : ICarMaintananceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICarServices carServices;

        public CarMaintananceService(IUnitOfWork unitOfWork, 
            IMapper mapper , 
            IHttpContextAccessor httpContextAccessor,
            ICarServices carServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.carServices = carServices;

        }
        public async Task AddMaintananceRecord(CarMaintananceAllDTO carMaintananceAllDTO)
        {

            if (carMaintananceAllDTO == null)
            {
                throw new CarMainTainanceNullException();
            }
            var idClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value;
            if (idClaim == null)
            {
                throw new CarMainTainanceNullException();
            }
            int carOwnerId = 0;
            if (!int.TryParse(idClaim, out carOwnerId))
            {
                throw new Exception("Invalid CarOwner Id in Claims.");
            }
            if (carOwnerId <= 0)
            {
                throw new CarMainTainanceNullException();
            }
            var carId = await carServices.GetCarIdByOwnerId(carOwnerId);
            carMaintananceAllDTO.CarId = carId;

            var repo =  unitOfWork.GetRepository<CarMaintenanceRecord, int>();
            var originCarMaintenanceRecord = mapper.Map<CarMaintenanceRecord>(carMaintananceAllDTO);
            if (originCarMaintenanceRecord != null)
            {
                await repo.AddAsync(originCarMaintenanceRecord);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new CarMainTainanceNullException();
            }
        }
    }
}
