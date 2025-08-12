using AutoMapper;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using SharedData.DTOs.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile.CarProfileMapping
{
    public class AddCarMapConfig : Profile
    {
        public AddCarMapConfig()
        {
            CreateMap<CreateCarDto, Car>()
                .ReverseMap();
            
        }
    }
    
}
