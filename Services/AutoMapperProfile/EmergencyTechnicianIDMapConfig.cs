using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.RequestsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class EmergencyTechnicianIDMapConfig : Profile
    {
        public EmergencyTechnicianIDMapConfig()
        {
            CreateMap<EmergencyTechnicianID, EmergencyRequest>().ReverseMap();
        }
    }
}
