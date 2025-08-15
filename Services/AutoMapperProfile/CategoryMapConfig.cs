using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs;
using SharedData.DTOs.Admin.TechnicianCategory;

namespace Service.AutoMapperProfile
{
    public class CategoryMapConfig : Profile
    {
        public CategoryMapConfig()
        {
            CreateMap<TCategory, TCategoryDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<TCategory, ReadTCategoryDTO>();
              
            CreateMap<CreateTCategoryDTO, TCategory>();



        }
    }
}
