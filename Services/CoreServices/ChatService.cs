using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMessegeService messegeService;
        public ChatService(IUnitOfWork unitOfWork, IMapper mapper, IMessegeService messegeService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.messegeService = messegeService;
        }


        //public async Task<List<ChatBreifDTO>> GetAllChatsAsync(int userId)
        //{
        //    var allChats = await unitOfWork.GetRepository<ChatSession, int>().GetAllAsync();
        //    if (allChats == null || !allChats.Any())
        //    {
        //        return null;
        //    }
        //    var userChats = allChats.Where(c => c.TechnicianId == userId && c.CarOwnerId == userId).ToList();
        //    if (userChats == null || !userChats.Any())
        //    {
        //        return null;
        //    }
        //    foreach (var chat in userChats)
        //    {
        //        var chatBreifDTOs = new ChatBreifDTO()
        //        {
        //            lastmessage = 

        //        }
        //    }
            



        //}
    }
}
