using AutoMapper;
using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.RequestsDTOs;
using SharedData.DTOs.TechnicianEmergencyRequestDTOs;

namespace Service.AutoMapperProfile
{
    public class PreRequestMapConfig : Profile
    {
        public PreRequestMapConfig()
        {

            CreateMap<CreatePreRequestDTO, EmergencyRequest>().ReverseMap();



            CreateMap<EmergencyRequestTechnicians, EmergencyRequestDetailsDTO>()
              .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.EmergencyRequestId))
              .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.EmergencyRequests.category.Name))
              .ForMember(dest => dest.CarOwnerId, opt => opt.MapFrom(src => src.EmergencyRequests.CarOwnerId))
              .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.EmergencyRequests.Latitude))
              .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.EmergencyRequests.Longitude))
              .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.EmergencyRequests.TimeStamp))
              .ForMember(dest => dest.EndTimeStamp, opt => opt.MapFrom(src => src.EmergencyRequests.EndTimeStamp))
             
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.EmergencyRequests.Description))
              .ForMember(dest => dest.CarOwnerName, opt => opt.MapFrom(src => src.EmergencyRequests.CarOwner.ApplicationUser.Name))
              .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.EmergencyRequests.CarOwner.ApplicationUser.FaceImageUrl))
              .ForMember(dest => dest.RequestState, opt => opt.MapFrom(src => src.CallStatus));
            CreateMap<EmergencyRequest, EmergencyRequestDetailsDTO>()
            .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.category.Name))
            .ForMember(dest => dest.CarOwnerName, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.Name))
            .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.FaceImageUrl));


            CreateMap<TechReverseRequest, TechReverseRequestDTO>()
                .ForMember(dest => dest.ReverseRequestId, opt => opt.MapFrom(src => src.Id))
                // auto mapper will map it from naming convention
                // .ForMember(dest => dest.TechnicianId, opt => opt.MapFrom(src => src.TechnicianId)) 
                .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.TimeStamp))
                .ForMember(dest => dest.CarOwnerRequestId, opt => opt.MapFrom(src => src.EmergencyRequestId))
                .ReverseMap();

        }


    }
}
