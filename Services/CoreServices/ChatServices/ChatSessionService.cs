using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation;
using Service.Specification_Implementation.ChatSessionsSpecifications;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ChatDTOs;
using SharedData.DTOs.ChatSessionDTOs;

namespace Service.CoreServices.ChatServices
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ChatSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        


        public async Task<ChatSessionAllDTO> GetChatSessions(int technicianId, int CarOwnerId)
        {
            var spec = new ChatSessionAllSpecification(technicianId ,CarOwnerId);

            var chatSessions = await unitOfWork.GetRepository<ChatSession, int>().GetAllWithSpecAsync(spec);
            if (chatSessions == null || !chatSessions.Any())
            {
                return null;
            }
            var chatsession = chatSessions.FirstOrDefault();
            return mapper.Map<ChatSessionAllDTO>(chatsession);
        }


        public async Task CompleteChatSession(int technicianId, int CarOwnerId)
        {
            var spec = new ChatSessionAllSpecification(technicianId, CarOwnerId);
            var Repo = unitOfWork.GetRepository<ChatSession, int>();
            var chatSessions = await Repo.GetAllAsync(spec);
            if (chatSessions == null || !chatSessions.Any())
            {
                throw new ChatSessionNotFoundException();
            }
            var chatsession = chatSessions.FirstOrDefault();
            if (chatsession == null)
            {
                throw new ChatSessionNotFoundException();
            }
            chatsession.IsClosed = true;
            chatsession.EndAt = DateTime.UtcNow;
            Repo.Update(chatsession);
        }

        public async Task<ChatSessionAllDTO> GetChatSessions(int ChatSessionId)
        {
            if (ChatSessionId <= 0)
            {
                throw new ArgumentException("ChatSessionId must be greater than zero.", nameof(ChatSessionId));
            }

            var chatSession = await unitOfWork.GetRepository<ChatSession, int>().GetByIdAsync(ChatSessionId);
            if (chatSession == null )
            {
                return null;
            }
            return mapper.Map<ChatSessionAllDTO>(chatSession);

        }

        public async Task<ChatSessionAllDTO> GetChatSessionsByCarOwnerId(int CarOwnerId)
        {
            var spec = new GetChatSessionByCarOwnerId(CarOwnerId);
            var chatSession = await unitOfWork.GetRepository<ChatSession , int> ().GetByIdAsync(spec);
            var mappedChatSession = mapper.Map<ChatSessionAllDTO>(chatSession);
            return mappedChatSession;

        }

        public async Task<ChatSessionAllDTO> GetChatSessionsByTechnicianId(int technicianId)
        {
            var spec = new GetChatSessionByTechnicianId(technicianId);
            var chatSession = await unitOfWork.GetRepository<ChatSession, int>().GetByIdAsync(spec);
            var mappedChatSession = mapper.Map<ChatSessionAllDTO>(chatSession);
            return mappedChatSession;

        }

        public async Task<ChatSession> GetOrCreateSessionAsync(int carOwnerId, int technicianId)
        {

            var chatRepo = unitOfWork.GetRepository<ChatSession, int>();
            var existing = await chatRepo.GetAllAsync(new ChatSessionBetweenParticipantsSpec(carOwnerId, technicianId));

            var open = existing.FirstOrDefault(cs => !cs.IsClosed);
            if (open != null) return open;

            var recent = existing.OrderByDescending(cs => cs.Id).FirstOrDefault();
            if (recent != null)
            {
                recent.IsClosed = false;
                chatRepo.Update(recent); 
                return recent;
            }

            var created = new ChatSession
            {
                StartAt = DateTime.UtcNow,
                IsClosed = false,
                CarOwnerId = carOwnerId,
                TechnicianId = technicianId
            };
            await chatRepo.AddAsync(created);
            return created;
        }

        public async Task createChatsession(int technicianId, int CarOwnerId)
        {
            ChatSession chatSession = new ChatSession()
            {
                CarOwnerId = CarOwnerId,
                StartAt = DateTime.UtcNow,
                TechnicianId = technicianId,
                IsClosed = false,
            };
            await unitOfWork.GetRepository<ChatSession, int>().AddAsync(chatSession);
                     
    }

        public async Task update(ChatSessionAllDTO chatSession)
        {
            var mappedchat = mapper.Map<ChatSession>(chatSession);
            unitOfWork.GetRepository<ChatSession, int>().Update(mappedchat);
            unitOfWork.SaveChangesAsync();
        }
    }
}
