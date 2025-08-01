using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using SharedData.DTOs.ChatDTOs;
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

        [HttpGet("GetChatById")]
        public IActionResult GetChatById([FromQuery] ChatBreifDTO chatBreif)
        {
            if (chatBreif == null || chatBreif.chatsessionid <= 0)
            {
                return BadRequest(ApiResponse<ChatDetailsDTO>.FailResponse("Invalid chat ID"));
            }
            var chat = serviceManager.ChatService.GetChatByIdAsync(chatBreif);
            if (chat == null || chat.Result == null)
            {
                return NotFound(ApiResponse<ChatDetailsDTO>.FailResponse("Chat not found"));
            }
            return Ok(ApiResponse<ChatDetailsDTO>.SuccessResponse(chat.Result, "Chat retrieved successfully"));
        }


    }
}
