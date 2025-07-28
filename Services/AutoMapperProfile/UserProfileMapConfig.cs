using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapperProfile
{
    public class UserProfileMapConfig : Profile
    {
        public UserProfileMapConfig()
        {

            CreateMap<Technician, ReadUserDetailsDTO>()
              .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.ApplicationUser.FaceImageUrl))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
              //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.TCategories.Selec))
              .ForMember(dest => dest.StartWorking, opt => opt.MapFrom(src => src.StartWorking))
              .ForMember(dest => dest.EndWorking, opt => opt.MapFrom(src => src.EndWorking))
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ApplicationUser.Address))
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ApplicationUser.Address))
              //.ForMember(dest => dest.Category, opt => opt.MapFrom((src, dest, destMember, context) =>
              // {
              //     // Resolve category from context.Items
              //     if (context.Items.TryGetValue("Category", out var categoryObj) && categoryObj is string Category)
              //     {
              //         var relation = src.TCategories.FirstOrDefault(e => e.Name == Category);
              //         return relation?.Name;
              //     }
              //     return null;
              // }));
              .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.TCategories.Select(c => c.Name).ToList()));



        }
    }
}
