//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.SignalR;
//using ServiceAbstraction;
//using ServiceAbstraction.CoreServicesAbstractions;

//namespace Presentation.Hubs
//{
//    public class ChatHub : BaseHub
//    {
//        private readonly IMessegeService messegeService;
//        private IHttpContextAccessor httpContextAccessor;
//        private IServiceManager ServiceManager { get; set; }

//        public ChatHub(IMessegeService messegeService) : base()
//        {
//            this.messegeService = messegeService;
//            this.httpContextAccessor = 
//            this.ServiceManager = 
//        }

 

//        public async Task JoinChatSession(int technicianId)
//        {
//            var userId = Context.User?.Claims
//                .FirstOrDefault(c => c.Type == "userId")?.Value;


//        }
//        // send massage 
//        public async Task SendMessage(string message, string senderId)
//        {
            
//        }


//    }
//}
