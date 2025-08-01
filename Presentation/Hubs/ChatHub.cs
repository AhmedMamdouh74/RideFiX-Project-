using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> UserRoom = new();
        public ChatHub() 
        {

        }
        

        // user go in room
        public async Task JoinRoom(string roomId)
        {
            if (UserRoom.TryGetValue(Context.ConnectionId, out var oldRoom))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, oldRoom);
                UserRoom[Context.ConnectionId] = roomId;
            }
            else
            {
                UserRoom.Add(Context.ConnectionId, roomId);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }
        // send massage 
        public async Task SendMessage(string message, string senderId)
        {
            var roomId = UserRoom[Context.ConnectionId];
            await Clients.Group(roomId).SendAsync("ReceiveMessage", new
            {
                Message = message,
                SenderId = senderId,
                RoomId = roomId
            });
        }
        // disconnect
        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }

        //public async Task sendmessage(string message)
        //{
        //    await Clients.All.SendAsync("recievemessage", message);
        //}
        //public override Task OnConnectedAsync()
        //{
        //    return base.OnConnectedAsync();
        //}
        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    return base.OnDisconnectedAsync(exception);
        //}
    }
}
