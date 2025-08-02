using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs;
using SharedData.DTOs.ConnectionDtos;

namespace Service.AutoMapperProfile
{
    public class ConnectionsIdDtoMappingProfile : Profile
    {
        public ConnectionsIdDtoMappingProfile()
        {
            CreateMap<UserConnectionIds, UserConnectionIdDto>().ReverseMap();
        }
    }
}
