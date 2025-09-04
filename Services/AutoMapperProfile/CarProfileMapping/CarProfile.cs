using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using SharedData.DTOs.Car;

namespace Service.AutoMapperProfile.CarProfileMapping
{
    internal class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDetailsDto>()
                .ForMember(dest => dest.ModelYear, opt => opt.MapFrom(src => src.modelYear.ToString()));
            ;
        }
    }
}
