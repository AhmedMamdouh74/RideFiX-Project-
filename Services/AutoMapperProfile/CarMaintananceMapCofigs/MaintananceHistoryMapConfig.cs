using AutoMapper;
using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using SharedData.DTOs.CarMaintananceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile.CarMaintananceMapCofigs
{
    public class MaintananceHistoryMapConfig : Profile
    {
        public MaintananceHistoryMapConfig()
        {
            CreateMap<CarMaintenanceRecord, MaintananceHistory>().ReverseMap();
        }
    }
}
