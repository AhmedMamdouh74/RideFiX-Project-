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
using SharedData.DTOs.ConnectionDtos;

namespace Presentation.Hubs
{
    public class RequestWatchDogHub : Hub
    {
        IServiceManager ServiceManager { get; set; }
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;

        public RequestWatchDogHub(IServiceManager servicemanager,
            IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            ServiceManager = servicemanager;
            this.httpContextAccessor = httpContextAccessor;
            this.unitOfWork = unitOfWork;

        }

        public async Task acceptrequest(int requestId)
        {

            var carOwner = await unitOfWork.GetRepository<EmergencyRequest, int>().GetByIdAsync(requestId);
            var carOwnerId = carOwner.CarOwnerId;
            var users = await ServiceManager.userConnectionIdService.SearchByCarOwnerId(carOwnerId);
            if (users != null && users.Any())
            {
                foreach (var user in users)
                {
                    await Clients.Client(user.ConnectionId).SendAsync("addreceivemessagelistener", "Request Accepted");
                }
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
            await ServiceManager.userConnectionIdService.AddAsync(connDto);
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
            await ServiceManager.userConnectionIdService.DeleteAsync(connDto);
            await base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
