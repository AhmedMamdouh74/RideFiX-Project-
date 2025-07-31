using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs;
using SharedData.DTOs.RequestsDTOs;
using SharedData.Wrapper;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public ChatController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllChats(int Id)
        //{

        //    return Ok(ApiResponse<RequestBreifDTO>.SuccessResponse(Request, "Has a Request"));
        //}
        [HttpGet("GetAllChats")]
        public IActionResult GetAllChhat()
        {
            var chats = serviceManager.ChatService.GetAllChatsAsync();
            if (chats == null || chats.Result == null)
            {
                return NotFound();
            }
            return Ok(ApiResponse<List<ChatBreifDTO>>.SuccessResponse(chats.Result, "Chats retrieved successfully"));
        }
        

    }
}
