using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.AspNetCore.Http;
using Service.Specification_Implementation.RequestSpecifications;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.Notifications;
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
       

        public ReverserRequestService(IUnitOfWork unitOfWork,
            IMapper mapper, 
            IRequestServices requestServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.requestServices = requestServices;
        

        }
        public Task AcceptRequest(int requestId)
        {
            throw new NotImplementedException();
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

        public Task RejectRequest(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
