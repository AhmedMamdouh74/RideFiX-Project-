using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.MessegeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class MessegeLastMessegeMapConfig : Profile
    {
        public MessegeLastMessegeMapConfig()
        {
            CreateMap<Message, MessegeDTO>().ReverseMap();
        }
    }
}
