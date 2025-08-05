using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Microsoft.AspNetCore.Http;
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

        public Task AddNewCar(CreateCarDto car)
        {
            throw new NotImplementedException();
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
                if (Car != null)
                {
                    var carDto = mapper.Map<CarDetailsDto>(Car?.FirstOrDefault());
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
    }
}
