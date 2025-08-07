using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Exception_Implementation.NotFoundExceptions;
using Service.Specification_Implementation;
using Service.Specification_Implementation.CarOwnerSpecifications;
using Service.Specification_Implementation.ChatSessionsSpecifications;
using Service.Specification_Implementation.TechnicianSpecifications;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.ChatDTOs;
using SharedData.DTOs.ChatSessionDTOs;
using SharedData.DTOs.MessegeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.CoreServices
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IChatSessionService chatSessionService;


        public ChatService(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IChatSessionService chatSessionService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.chatSessionService = chatSessionService;
        }


        public async Task<List<ChatBreifDTO>> GetAllChatsAsync()
        {
            string lastmessege = string.Empty;
            var userChats = new List<ChatSession>();
            var user = httpContextAccessor.HttpContext;
            if (user == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            // Determine if the user is a Car Owner or Technician
            var userRole = user.User.Claims.FirstOrDefault(s => s.Type == "Role")?.Value;
            var ApplicationUser = user.User.Claims.FirstOrDefault(s => s.Type == "userId")?.Value;
            int entityId;
            bool isValid = int.TryParse(user.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value, out entityId);
            if (!isValid)
            {
                return null;
            }

            if (string.IsNullOrEmpty(userRole))
            {
                throw new UnauthorizedAccessException("User role is not specified.");
            }
          

            if (userRole == "CarOwner")
            {             
            var CarSpec = new CarOwnerChatSpecification(entityId);
                var carOwnerChat = await unitOfWork.GetRepository<ChatSession, int>().GetAllAsync(CarSpec);
                if (carOwnerChat == null)
                {
                    return null;
                }
                userChats = carOwnerChat.ToList();
            }
            else if (userRole == "Technician")
            {
            var TechSpec = new TechnicianChatSpecification(entityId);
                var technicianChat = await unitOfWork.GetRepository<ChatSession, int>().GetAllAsync(TechSpec);
                if (technicianChat == null)
                {
                    return null;
                }
                userChats = technicianChat.ToList();
            }
            else
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");
            }

            if (userChats == null || !userChats.Any())
            {
                return null;
            }
            List<ChatBreifDTO> chatBreifDTOs = new List<ChatBreifDTO>();
            foreach (var chat in userChats)
            {
                if(chat.massages == null || !chat.massages.Any())
                {
                    continue; 
                }
                lastmessege = chat.massages?.FirstOrDefault().Text;
                var chatBreifDTO = new ChatBreifDTO()
                {
                    name = chat.CarOwner.ApplicationUser.Name,
                    imgurl = chat.CarOwner.ApplicationUser.FaceImageUrl,
                    chatsessionid = chat.Id,
                    lastmessage = lastmessege
                };
                chatBreifDTOs.Add(chatBreifDTO);
            }
            return chatBreifDTOs;
        }

        public async Task<ChatDetailsDTO> GetChatByIdAsync(int chatsessionid)
        {
            if (chatsessionid <= 0)
            {
                return null;
            }
            var spec = new ChatDetailsSpecification(chatsessionid);
            var chatSession = await unitOfWork.GetRepository<ChatSession, int>().GetByIdAsync(spec);
        
            if (chatSession == null)
            {
                throw new ChatNotFoundException();
            }
            var messages = chatSession.massages.ToList();
            var mappedMessages = mapper.Map<List<MessegeDTO>>(messages);
            var user = httpContextAccessor.HttpContext;
            if (user == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userRole = user.User.Claims.FirstOrDefault(s => s.Type == "Role")?.Value;
            if (userRole == null)
            {
                throw new UnauthorizedAccessException("User role is not specified.");
            }
            string User = string.Empty;
            string imgurl = string.Empty;
            if (userRole == "CarOwner")
            {
                User = chatSession.Technician.ApplicationUser.Name;
                imgurl = chatSession.Technician.ApplicationUser.FaceImageUrl;
            }
            else if (userRole == "Technician")
            {
                User = chatSession.CarOwner.ApplicationUser.Name;
                imgurl = chatSession.CarOwner.ApplicationUser.FaceImageUrl;
            }
            else
            {
                throw new UnauthorizedAccessException("User is not authorized to access this resource.");

            }
                var chatDetails = new ChatDetailsDTO()
            {
                name = User,
                imgurl = imgurl,
                messages = mappedMessages

            };

            return chatDetails;

        }

        public async Task<ChatSessionAllDTO> LoadCurrentChat()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var userRole = user.Claims.FirstOrDefault(s => s.Type == "Role")?.Value;
            int userId = 0;
            var idClaim = user?.Claims.FirstOrDefault(s => s.Type == "Id")?.Value;
            int.TryParse(idClaim, out userId);

            if (userRole == null)
            {
                throw new UnauthorizedAccessException("User role is not specified.");
            }
            else if(userRole == "CarOwner")
            {
               var currentchat = await chatSessionService.GetChatSessionsByCarOwnerId(userId);
                if (currentchat == null)
                {
                    throw new ChatNotFoundException();
                }
                return currentchat;
            } else if (userRole == "Technician")
            {
               var currentchat = await chatSessionService.GetChatSessionsByTechnicianId(userId);
                if (currentchat == null)
                {
                    throw new ChatNotFoundException();
                }
                return currentchat;
            }
            throw new UnauthorizedAccessException("User is not authorized to access this resource.");

        }


    }
}
