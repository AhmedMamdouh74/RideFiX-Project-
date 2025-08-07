using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using SharedData.DTOs.CarMaintananceDTOs;
using SharedData.DTOs.MTypesDtos;

namespace Service.AutoMapperProfile.CarMaintananceMapCofigs
{
    public class CarMaintananceAllMapCofig : Profile
    {
        public CarMaintananceAllMapCofig()
        {
            CreateMap<CarMaintenanceRecord, CarMaintananceAllDTO>()
                .ForMember(dest => dest.MaintenanceType, opt => opt.MapFrom(src => src.MaintenanceType))
                .ReverseMap();
            CreateMap<MaintenanceTypes, MaintenanceTypeDTO>()
                .ReverseMap();
            CreateMap<MaintenanceTypes, MaintenanceTypeDetailsDto>()
                .ReverseMap();
        }
    }
}
