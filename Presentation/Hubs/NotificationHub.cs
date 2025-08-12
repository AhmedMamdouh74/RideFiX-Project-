using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ConnectionDtos;
using SharedData.DTOs.Notifications;
using SharedData.DTOs.RequestsDTOs;

namespace Presentation.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IServiceManager serviceManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;

        public NotificationHub(IServiceManager serviceManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            this.serviceManager = serviceManager;
            this.httpContextAccessor = httpContextAccessor;
            this.unitOfWork = unitOfWork;
        }

        public async Task offerrequest(int requestId)
        {
            var userToken = httpContextAccessor.HttpContext?.User;
            int userId = 0;
            var idClaim = userToken?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int.TryParse(idClaim, out userId);

            var carOwner = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(requestId);
            var carOwnerId = carOwner.CarOwnerId;

            var Tech = await serviceManager.technicianService.GetTechnicianByIdAsync(userId);
            var users = await serviceManager.userConnectionIdService.SearchByCarOwnerId(carOwnerId);            
            foreach (var user in users)
            {
                await Clients.Client(user.ConnectionId).SendAsync("recievenotification", new NotificationDto()
                {
                    Name = Tech.Name,
                    GovernmentName = Tech.government,
                    RequestId = requestId
                });
            }
        }

        #region Overriding
        public async override Task OnConnectedAsync()
        {
            var user = httpContextAccessor.HttpContext;
            //var userId = Context.User?.Claims
            //    .FirstOrDefault(c => c.Type == "userId")?.Value;
            var userId = user.User.Claims.FirstOrDefault(s => s.Type == "userId")?.Value;

            var connDto = new UserConnectionIdDto()
            {
                ApplicationUserId = userId,
                ConnectionId = Context.ConnectionId,
            };
            await serviceManager.userConnectionIdService.AddAsync(connDto);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.Claims
                            .FirstOrDefault(c => c.Type == "userId")?.Value;

            var connDto = new UserConnectionIdDto()
            {
                ApplicationUserId = userId,
                ConnectionId = Context.ConnectionId,
            };
            await serviceManager.userConnectionIdService.DeleteAsync(connDto);
            await base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
