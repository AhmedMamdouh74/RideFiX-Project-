using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.ChatSessionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class ChatSessionAllMapConfig : Profile
    {
        public ChatSessionAllMapConfig()
        {
            CreateMap<ChatSession, ChatSessionAllDTO>()
            .ForMember(dest => dest.messages, opt => opt.MapFrom(src => src.massages))
            .ReverseMap();

        }
    }
}
