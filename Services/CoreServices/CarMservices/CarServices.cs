using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Http;
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

        public Task<CarDetailsDto> GetCarDetailsAsync()
        {
            var user = httpContextAccessor.HttpContext;
            var roleId = user.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value;
            throw new NotImplementedException();
        }
    }
}
