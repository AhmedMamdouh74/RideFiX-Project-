
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Service.Exception_Implementation.ArgumantNullException;
using ServiceAbstraction.CoreServicesAbstractions.CarMservices;
using SharedData.DTOs.Car;
using SharedData.DTOs.CarMaintananceDTOs;
using SharedData.DTOs.MTypesDtos;

namespace Service.CoreServices.CarMservices
{
    public class CarMaintananceService : ICarMaintananceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICarServices carServices;
        private readonly IMaintenanceTypesService maintenanceTypesService;
        private readonly IEmailService emailService;

        public CarMaintananceService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ICarServices carServices,
            IMaintenanceTypesService maintenanceTypesService,
            IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.carServices = carServices;
            this.maintenanceTypesService = maintenanceTypesService;
            this.emailService = emailService;
        }

        public async Task AddMaintananceRecord(CarMaintananceAllDTO carMaintananceAllDTO)
        {
            try
            {
                var idClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value;
                var emailClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(s => s.Type == "Email")?.Value;
                var nameClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(s => s.Type == "Name")?.Value;

                #region Validate of Input
                if (idClaim == null)
                {
                    throw new CarMainTainanceNullException();
                }
                if (!int.TryParse(idClaim, out var carOwnerId))
                {
                    throw new Exception("Invalid CarOwner Id in Claims.");
                }
                if (carOwnerId <= 0)
                {
                    throw new CarMainTainanceNullException();
                }
                #endregion

                #region Helper Objects

                var car = await carServices.GetCarDetailsAsync();
                var maintenanceType = await maintenanceTypesService.GetMTypebyIdAsync(carMaintananceAllDTO.MaintenanceTypeId);
                if (maintenanceType == null)
                {
                    throw new CarMainTainanceNullException();
                }

                #endregion

                var NextMDate = DueDateCalculateAsync(maintenanceType, carMaintananceAllDTO.PerformedAt, car);

                #region Initialize CarMaintainance Data

                var originCarMaintenanceRecord = mapper.Map<CarMaintenanceRecord>(carMaintananceAllDTO);
                originCarMaintenanceRecord.NextMaintenanceDue = NextMDate;
                originCarMaintenanceRecord.CarId = car.Id;

                #endregion

                ScheduleMaintainanceReminder(emailClaim!,
                    maintenanceType.Name,
                    nameClaim!,
                    NextMDate);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("Server Error or Invalid input Data");
            }
        }


        private DateTime DueDateCalculateAsync(MaintenanceTypeDetailsDto maintenanceType, DateTime Mdate, CarDetailsDto car)
        {
            if (maintenanceType?.RepeatEveryKM != null)
            {
                var TimeInMonths = (int)(maintenanceType?.RepeatEveryKM / car?.AvgKmPerMonth)!;  // الشهور المطلوبة
                DateTime nextMaintenanceDate = Mdate.AddMonths(TimeInMonths);
                return nextMaintenanceDate;
            }
            else if (maintenanceType?.RepeatEveryDays != null)
            {
                DateTime nextMaintenanceDate = Mdate.AddDays((int)maintenanceType?.RepeatEveryDays!);
                return nextMaintenanceDate;
            }
            else
            {
                throw new ArgumentException("Error in calculating");
            }
        }

        private void ScheduleMaintainanceReminder(string toEmail, string maintananceType, string ownername, DateTime maintananceDate)
        {
            if (string.IsNullOrEmpty(toEmail) ||
                string.IsNullOrEmpty(maintananceType) ||
                string.IsNullOrEmpty(ownername) ||
                maintananceDate == default)
            {
                throw new ArgumentException("Email, maintenance type, owner name, and maintenance date cannot be null");
            }

            DateTime currentDate = DateTime.Today;
            if (currentDate >= maintananceDate)
            {
                return;
            }
            int daysDifference = (maintananceDate - currentDate).Days;

            BackgroundJob.Schedule(() =>
                        emailService.SendEmail(toEmail, maintananceType, ownername, DateOnly.FromDateTime(maintananceDate)),
                        TimeSpan.FromDays(daysDifference));
        }
    }
}



