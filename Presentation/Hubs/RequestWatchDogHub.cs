using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ServiceAbstraction;
using SharedData.DTOs.ConnectionDtos;

namespace Presentation.Hubs
{
    public class RequestWatchDogHub : Hub
    {
        IServiceManager ServiceManager { get; set; }
        public RequestWatchDogHub(IServiceManager servicemanager)
        {
            ServiceManager = servicemanager;
        }

        #region Overriding
        public async override Task OnConnectedAsync()
        {
            var userId = Context.User?.Claims
                .FirstOrDefault(c => c.Type == "userId")?.Value;

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
