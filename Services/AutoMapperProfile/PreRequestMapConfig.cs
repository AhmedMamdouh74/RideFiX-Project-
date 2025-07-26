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


			//CreateMap<CreatePreRequestDTO, EmergencyRequest>()
			//    .ForMember(des => des.CarOwner.ApplicationUser.PIN, opt => opt.MapFrom(src => src.PIN));

            CreateMap<EmergencyRequest, EmergencyRequestDetailsDTO>()
             .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.CarOwnerName, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.Name))
             .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.FaceImageUrl))
             .ForMember(dest => dest.RequestState, opt => opt.MapFrom((src, dest, destMember, context) =>
             {
                 // Resolve technicianId from context.Items
                 if (context.Items.TryGetValue("TechnicianId", out var techIdObj) && techIdObj is int technicianId)
                 {
                     var relation = src.EmergencyRequestTechnicians.FirstOrDefault(e => e.TechnicianId == technicianId);
                     return relation?.CallStatus;
                 }
                 return null;
             }))
              .ForMember(dest => dest.TechnicianId, opt => opt.MapFrom((src, dest, destMember, context) =>
              {
                  if (context.Items.TryGetValue("TechnicianId", out var techIdObj) && techIdObj is int technicianId)
                  {
                      return technicianId;
                  }
                  return (int?)null;
              }))
              .ReverseMap();
            CreateMap<EmergencyRequestTechnicians, EmergencyRequestDetailsDTO>()
              .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.EmergencyRequestId))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.EmergencyRequests.Description))
              .ForMember(dest => dest.CarOwnerName, opt => opt.MapFrom(src => src.EmergencyRequests.CarOwner.ApplicationUser.Name))
              .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.EmergencyRequests.CarOwner.ApplicationUser.FaceImageUrl))
              .ForMember(dest => dest.TechnicianId, opt => opt.MapFrom(src => src.TechnicianId))
              .ForMember(dest => dest.RequestState, opt => opt.MapFrom(src => src.CallStatus));
            CreateMap<EmergencyRequest, EmergencyRequestDetailsDTO>()
            .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CarOwnerName, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.Name))
            .ForMember(dest => dest.FaceImageUrl, opt => opt.MapFrom(src => src.CarOwner.ApplicationUser.FaceImageUrl));
            ////  .ForMember(dest => dest.TechnicianId, opt => opt.MapFrom(src => src.TechnicianId));


            CreateMap<TechReverseRequest, TechReverseRequestDTO>()
				.ForMember(dest => dest.ReverseRequestId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.TechnicianId, opt => opt.MapFrom(src => src.TechnicianId))
				.ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => src.TimeStamp))
				.ForMember(dest => dest.CarOwnerRequestId, opt => opt.MapFrom(src => src.EmergencyRequestId))
				.ReverseMap();

		}


	}
}
