using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.ChatDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class ChatDetailsMapConfig : Profile
    {
        //public ChatDetailsMapConfig()
        //{
        //    CreateMap<ChatDetailsDTO, ChatSession>()
        //        .ForMember(dest => dest.ApplicationUser.Name, opt => opt.MapFrom(src => src.name))
        //        .ForMember(dest => dest.ApplicationUser.FaceImageUrl, opt => opt.MapFrom(src => src.imgurl));
        //}
    }
}
