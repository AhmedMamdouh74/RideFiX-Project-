using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.AspNetCore.Http;
using Service.Exception_Implementation.ArgumantNullException;
using Service.Specification_Implementation.RequestSpecifications;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.Notifications;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices.EmergencyReqServices
{
    public class ReverserRequestService : IReverserRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IRequestServices requestServices;
        private readonly IChatSessionService chatSessionService;
       

        public ReverserRequestService(IUnitOfWork unitOfWork,
            IMapper mapper, 
            IRequestServices requestServices,
            IChatSessionService chatSessionService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.requestServices = requestServices;
            this.chatSessionService = chatSessionService;
        }
        public async Task AcceptRequest(int requestId)
        {
            if (requestId <= 0)
            {
                throw new ReverseArgumentException();
            }
            var spec = new AcceptReverseRequestSpecification(requestId);
            var reverseRequest = await unitOfWork.GetRepository<TechReverseRequest, int>().GetByIdAsync(spec);

            if (reverseRequest == null)
            {
                throw new ReverseArgumentException("Reverse request not found");
            }
            if (reverseRequest.EmergencyRequest.TechnicianId != null)
            {
                throw new ReverseArgumentException("This request has already been accepted by another technician.");
            }
            reverseRequest.EmergencyRequest.TechnicianId = reverseRequest.TechnicianId;
            reverseRequest.CallState = RequestState.Answered;

            var chatSession = await chatSessionService.GetChatSessions(reverseRequest.TechnicianId, reverseRequest.EmergencyRequest.CarOwnerId);
            if (chatSession == null)
            {
                await chatSessionService.createChatsession(reverseRequest.TechnicianId, reverseRequest.EmergencyRequest.CarOwnerId);
            }
            else
            {
                chatSession.IsClosed = false;
                chatSession.EndAt = null;
                chatSessionService.update(chatSession);
                
            }
           
            await unitOfWork.SaveChangesAsync();

        }

        public async Task<List<NotificationDto>> GetReverserequest()
        {
            var CurrentRequestId = requestServices.GetCurrentRequestId().Result;
            if (CurrentRequestId == 0)
            {
                throw new Exception("No current request found.");
            }

            var spec = new GetReverseRequestSpecification(CurrentRequestId);
            var reverseRequests = await unitOfWork.GetRepository<TechReverseRequest, int>().GetAllWithSpecAsync(spec);
            if (reverseRequests == null || !reverseRequests.Any())
            {
                return new List<NotificationDto>();
            }
            var notificationDtos = new List<NotificationDto>();
            foreach (var reverseRequest in reverseRequests)
            {
                NotificationDto notification = new NotificationDto();
                notification.Name = reverseRequest.Technician.ApplicationUser.Name;
                notification.GovernmentName = reverseRequest.Technician.government.ToString();
                notification.RequestId = reverseRequest.Id;

                notificationDtos.Add(notification);
            }

            return notificationDtos;


        }

        public async Task RejectRequest(int requestId)
        {
            if (requestId <= 0)
            {
                throw new ReverseArgumentException();
            }
            var spec = new AcceptReverseRequestSpecification(requestId);
            var reverseRequest = await unitOfWork.GetRepository<TechReverseRequest, int>().GetByIdAsync(spec);
            if (reverseRequest == null)
            {
                throw new ReverseArgumentException("Reverse request not found.");
            }
            reverseRequest.CallState = RequestState.Rejected;

            await unitOfWork.SaveChangesAsync();
        }
    }
}
