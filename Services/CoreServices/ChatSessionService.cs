using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ChatDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class ChatSessionService : IChatSessionService
    {
        public Task<ChatDetailsDTO> GetChatById(int chatSessionId)
        {
            throw new NotImplementedException();
        }
    }
}
