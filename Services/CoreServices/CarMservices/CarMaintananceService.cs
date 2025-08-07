using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Microsoft.AspNetCore.Http;
using Service.Exception_Implementation.ArgumantNullException;
using Service.Exception_Implementation.NotFoundExceptions;
using ServiceAbstraction.CoreServicesAbstractions.CarMservices;
using SharedData.DTOs.CarMaintananceDTOs;

namespace Service.CoreServices.CarMservices
{
    public class CarMaintananceService : ICarMaintananceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICarServices carServices;
        private readonly IMaintenanceTypesService maintenanceTypesService;

        public CarMaintananceService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ICarServices carServices,
            IMaintenanceTypesService maintenanceTypesService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.carServices = carServices;
            this.maintenanceTypesService = maintenanceTypesService;
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

            var repo = unitOfWork.GetRepository<CarMaintenanceRecord, int>();
            var originCarMaintenanceRecord = mapper.Map<CarMaintenanceRecord>(carMaintananceAllDTO);
            if (originCarMaintenanceRecord != null)
            {
                await repo.AddAsync(originCarMaintenanceRecord);
                var mRepo = unitOfWork.GetRepository<CarMaintenanceRecord, int>();
                //var maintenanceType = await mRepo.GetByIdAsync();
                //if (maintenanceType != null)
                //{
                //    var car = unitOfWork.GetRepository<Car, int>().GetByIdAsync(carId);
                //    if (car != null)
                //    {
                //        var Date = DueDateCalculate(maintenanceType, car);

                //    }
                //}
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new CarMainTainanceNullException();
            }
        }


        private DateTime DueDateCalculate(MaintenanceTypes maintenanceType, Car car)
        {
            if (maintenanceType.RepeatEveryKM != null)
            {
                double TimeInDays = (double)(maintenanceType.RepeatEveryKM / car.AvgKmPerMonth) * 30;  // الشهور المطلوبة
                DateTime nextMaintenanceDate = DateTime.Now.AddDays(TimeInDays);
                return nextMaintenanceDate;
            }
            else if (maintenanceType.RepeatEveryDays != null)
            {
                DateTime nextMaintenanceDate = DateTime.Now.AddDays((double)maintenanceType.RepeatEveryDays);
                return nextMaintenanceDate;
            }
            else
            {
                throw new ArgumentException("Error in calculating");
            }
        }
    }
}
