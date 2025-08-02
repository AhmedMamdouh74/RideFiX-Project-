using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ServiceAbstraction;
using SharedData.DTOs.ConnectionDtos;

namespace Presentation.Hubs
{
    public class RequestWatchDogHub : BaseHub
    {
        IServiceManager ServiceManager { get; set; }
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequestWatchDogHub(IServiceManager servicemanager,
            IHttpContextAccessor httpContextAccessor)
        {
            ServiceManager = servicemanager;
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task acceptrequest(int CarOwnerId)
        {
            var users = await ServiceManager.userConnectionIdService.SearchByCarOwnerId(CarOwnerId);
            if (users != null && users.Any())
            {
                foreach (var user in users)
                {
                    await Clients.Client(user.ConnectionId).SendAsync("addreceivemessagelistener", "Request Accepted");
                }
            }

        }



    }
}
