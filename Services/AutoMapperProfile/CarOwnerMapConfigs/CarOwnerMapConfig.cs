using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.Admin.Users;

namespace Service.AutoMapperProfile.CarOwnerMapConfigs
{
    public class CarOwnerMapConfig : Profile

    {
        public CarOwnerMapConfig()
        {
            CreateMap<CarOwner, ReadUsersDTO>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.IsActivated, opt => opt.MapFrom(src => src.ApplicationUser.IsActivated))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ApplicationUser.FaceImageUrl))
        .ForMember(dest => dest.Rate, opt => opt.MapFrom(src =>
            src.Reviews.Any()
                ? (int)Math.Round(src.Reviews.Average(r => r.Rate))
                : 0
        ));
        }

    }
}
