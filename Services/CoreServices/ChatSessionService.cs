using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ChatDTOs;
using SharedData.DTOs.ChatSessionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public async Task<ChatSessionAllDTO> GetChatSessions(int technicianId , int CarOwnerId)
        {
            var spec = new ChatSessionAllSpecification(CarOwnerId, technicianId);
            var chatSessions = await unitOfWork.GetRepository<ChatSession, int>().GetAllAsync(spec);
            if (chatSessions == null || !chatSessions.Any())
            {
                return null;
            }
            var chatsession = chatSessions.FirstOrDefault();
            return mapper.Map<ChatSessionAllDTO>(chatsession);
        }

    }
}
