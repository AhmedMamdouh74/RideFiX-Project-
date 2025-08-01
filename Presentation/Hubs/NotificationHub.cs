using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.RequestsDTOs;

namespace Presentation.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IRequestServices requestServices;
        public NotificationHub(IRequestServices requestServices)
        {
            this.requestServices = requestServices;
        }

        public async Task SendRequestFromClient(RealRequestDTO request)
        {
            await requestServices.CreateRealRequest(request);
            // Notify all connected clients about the new request
            foreach (var technicianId in request.TechnicianIDs)
            {
                await Clients.User(technicianId.ToString()).SendAsync("ReceiveRequest", new {
                    title = "New Emergency Request",
                    description = request.Description,
                    categoryId = request.categoryId
                });
            }
        }
    }
}
