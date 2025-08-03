using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ConnectionDtos;
using SharedData.DTOs.MessegeDTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Presentation.Hubs
{
    public class ChatHub : Hub
    {
        private IHttpContextAccessor httpContextAccessor;
        private IServiceManager ServiceManager { get; set; }

        public ChatHub(IHttpContextAccessor httpContextAccessor, IServiceManager service)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.ServiceManager = service;
        }

        public async Task sendmessage(int chatsessionId , string messege)
        {
            var user = httpContextAccessor.HttpContext?.User;
            int userId = 0;
            var idClaim = user?.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int.TryParse(idClaim, out userId);
            string otherUserId = string.Empty;
            var UserConcId = Context.ConnectionId;
            string ApplicationId = string.Empty;
            var chatSession = await ServiceManager.chatSessionService.GetChatSessions(chatsessionId);
            if (chatSession == null)
            {
                throw new InvalidOperationException("Chat session not found.");
            }

            string UserRole = user?.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            if(UserRole == "CarOwner")
            {             
                var technicianId = chatSession.TechnicianId;
                var techinicain = await ServiceManager.userConnectionIdService.SearchByTechnichanId(technicianId);

                if (techinicain == null || !techinicain.Any())
                {
                    throw new InvalidOperationException("No technician found for this chat session.");
                }
                ApplicationId = techinicain.FirstOrDefault()?.ApplicationUserId;


                otherUserId = techinicain.FirstOrDefault()?.ConnectionId;
            }
            else if (UserRole == "Technician")
            {
                var CarOwnerId = chatSession.CarOwnerId;
                var carOwners = await ServiceManager.userConnectionIdService.SearchByCarOwnerId(CarOwnerId);
                if (carOwners == null)
                {
                    throw new InvalidOperationException("No car owner found for this chat");
                }

                ApplicationId = carOwners.FirstOrDefault()?.ApplicationUserId;
                otherUserId = carOwners.FirstOrDefault()?.ConnectionId;

            }
            else
            {
                throw new UnauthorizedAccessException("User role is not valid for sending messages.");
            }

            var message = new MessegeAllDTO
            {
                ChatSessionId = chatsessionId,
                Text = messege,
                ApplicationId = ApplicationId,
                SentAt = DateTime.UtcNow,

            };
            await ServiceManager.messegeService.AddMessegeAsync(message);

            var sendMessege = new MessegeMiniDTO()
            {
                Text = messege,
                SentAt = DateTime.UtcNow,
                IsSeen = false,
                ApplicationId = ApplicationId
            };


            //await Clients.Client(UserConcId).SendAsync("ReceiveMessage", sendMessege);
            await Clients.Client(otherUserId).SendAsync("ReceiveMessage", sendMessege);

        }


        #region Overriding
        public async override Task OnConnectedAsync()
        {
            var user = httpContextAccessor.HttpContext;

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
