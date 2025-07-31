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
    public class RequestBreifMapConfig : Profile
    {
        public RequestBreifMapConfig()
        {
            CreateMap<EmergencyRequest, RequestBreifDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TechnicianName, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.category.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.TechnicianName, opt => opt.MapFrom(
                src => src.Technician != null && src.Technician.ApplicationUser != null
                    ? src.Technician.ApplicationUser.Name
                    : null
            ));
        }
    }
}
