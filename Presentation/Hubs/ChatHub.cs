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
        private Dictionary<string,string> keyValuePairs = new Dictionary<string,string>();
        public ChatHub() 
        {

        }
        public async Task sendmessage(string message)
        {
            await Clients.All.SendAsync("recievemessage", message);
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
