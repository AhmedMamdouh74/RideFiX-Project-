using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Exception_Implementation.BadRequestExceptions;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation;
using Service.Specification_Implementation.CarSpecification;
using SharedData.DTOs.Car;

namespace Service.CoreServices.CarMservices
{
    public class CarServices : ICarServices
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CarServices(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task AddNewCar(CreateCarDto cardto)
        {
            if (cardto == null)
            {
                throw new CarBadRequestException();
            }
            var user = httpContextAccessor.HttpContext;
            if (user == null)
            {
                throw new CarBadRequestException("no authorize");
            }
            var flag = int.TryParse(user.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value
                        , out var ownerId);
            if (!flag)
            {
                throw new CarBadRequestException("there ids no ownerId");
            }
            if (ownerId <= 0)
            {
                throw new CarBadRequestException();
            }
            var spec = new CarIdSpecification(ownerId);
            var car = await unitOfWork.GetRepository<Car, int>().GetAllAsync(spec);
            if (car != null && car.Any())
            {
                throw new CarBadRequestException("you already have a car");
            }
            var carRepo = unitOfWork.GetRepository<Car, int>();
            var carEntity = mapper.Map<Car>(cardto);
            carEntity.OwnerId = ownerId;
            await carRepo.AddAsync(carEntity);
            await unitOfWork.SaveChangesAsync();

        }

        public async Task<CarDetailsDto> GetCarDetailsAsync()
        {
            var user = httpContextAccessor.HttpContext;
            if (user == null)
            {
                throw new CarBadRequestException();
            }
            var flag = int.TryParse(user.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value
                        , out var roleId);
            if (flag)
            {
                var Repo = unitOfWork.GetRepository<Car, int>();
                var spec = new CarByOwnerIdSpecification(roleId);
                var Car = await Repo.GetAllAsync(spec);
                var car = Car.FirstOrDefault();
                if (Car != null)
                {
                    var carDto = mapper.Map<CarDetailsDto>(car);
                    carDto.DaysSinceLastMaintenance =
                        car.LastMaintenanceDate.HasValue
                            ? (DateTime.Now - car.LastMaintenanceDate.Value).Days
                            : 0;
                    return carDto;
                }
                else
                {
                    throw new CarNotFoundException();
                }
            }
            else
            {
                throw new CarBadRequestException();
            }
        }

        public async Task<int> GetCarIdByOwnerId(int ownerId)
        {
            if (ownerId <= 0)
            {
                throw new CarBadRequestException();
            }
            var spec = new CarIdSpecification(ownerId);
            var car = await unitOfWork.GetRepository<Car, int>().GetAllAsync(spec);
            if (car == null || !car.Any())
            {
                throw new CarNotFoundException();
            }
            return car.FirstOrDefault().Id;

        }

        public async Task SetCarStats(DateTime date, decimal cost, int CarId)
        {
            var carRepo = unitOfWork.GetRepository<Car, int>();
            var car = await carRepo.GetByIdAsync(CarId);
            if (car == null)
            {
                throw new CarNotFoundException();
            }
            car.TotalMaintenanceCost += cost;
            car.MaintenanceCount++;
            car.LastMaintenanceDate = date;
        }

        public async Task DeleteCar()
        {
            var user = httpContextAccessor.HttpContext;
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            var flag = int.TryParse(user.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value
                        , out var ownerId);
            if (!flag)
            {
                throw new CarBadRequestException("there is no ownerId");
            }
            int carID = await GetCarIdByOwnerId(ownerId);
            await unitOfWork.GetRepository<Car, int>().DeleteAsync(carID);
            await unitOfWork.SaveChangesAsync();
           
            }
    }
}
