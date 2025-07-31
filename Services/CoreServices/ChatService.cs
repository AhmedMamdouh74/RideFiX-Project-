using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Service.Specification_Implementation;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs;
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
        private readonly IMessegeService messegeService;
        private readonly IHttpContextAccessor httpContextAccessor;

      
        public ChatService(IUnitOfWork unitOfWork, IMapper mapper, 
            IMessegeService messegeService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.messegeService = messegeService;
            this.httpContextAccessor = httpContextAccessor;
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
            var entityId = int.Parse(user.User.Claims.FirstOrDefault(s => s.Type == "Id")?.Value);
            if (string.IsNullOrEmpty(userRole))
            {
                throw new UnauthorizedAccessException("User role is not specified.");
            }
          
            var CarSpec = new CarOwnerChatSpecification(entityId);
            var TechSpec = new TechnicianChatSpecification(entityId);

            if (userRole == "CarOwner")
            {             
                var carOwnerChat = await unitOfWork.GetRepository<ChatSession, int>().GetAllAsync(CarSpec);
                if (carOwnerChat == null)
                {
                    return null;
                }
                userChats = carOwnerChat.ToList();
            }
            else if (userRole == "Technician")
            {
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
                var allmessege = await messegeService.GetAllMessegesAsync(chat.Id);
                if (allmessege == null || !allmessege.Any())
                {
                    lastmessege = "No messages yet";
                }
                else
                {
                    lastmessege = allmessege.FirstOrDefault().Text;
                }
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

       
    }
}
