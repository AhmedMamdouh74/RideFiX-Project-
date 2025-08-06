using Domain.Entities.CoreEntites.EmergencyEntities;
using SharedData.DTOs.ChatDTOs;
using SharedData.DTOs.ChatSessionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IChatSessionService
    {
        public Task<ChatSessionAllDTO> GetChatSessions(int technicianId, int CarOwnerId);
        public Task<ChatSessionAllDTO> GetChatSessionsByCarOwnerId(int CarOwnerId);

        public Task<ChatSessionAllDTO> GetChatSessionsByTechnicianId(int technicianId);

        public Task<ChatSessionAllDTO> GetChatSessions(int ChatSessionId);

        Task<ChatSession> GetOrCreateSessionAsync(int carOwnerId, int technicianId);

        public Task CompleteChatSession(int technicianId, int CarOwnerId);
    }
}
