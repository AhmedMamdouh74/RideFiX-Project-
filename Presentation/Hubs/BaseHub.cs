using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ServiceAbstraction;
using SharedData.DTOs.ConnectionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Hubs
{
    public class BaseHub : Hub
    {
        private IHttpContextAccessor httpContextAccessor;
        private IServiceManager ServiceManager { get; set; }

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
