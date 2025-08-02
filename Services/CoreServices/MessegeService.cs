using AutoMapper;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Service.Exception_Implementation.ArgumantNullException;
using ServiceAbstraction.CoreServicesAbstractions;
using SharedData.DTOs.MessegeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CoreServices
{
    public class MessegeService : IMessegeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public MessegeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddMessegeAsync(MessegeAllDTO messegeDTO)
        {
            if (messegeDTO == null)
            {
                throw new MessegeNullException();
            }
            var message = new Message
            {

                ChatSessionId = messegeDTO.ChatSessionId,
                Text = messegeDTO.Text,
                SentAt = DateTime.UtcNow,
                ApplicationId = messegeDTO.ApplicationId
            };
            await unitOfWork.GetRepository<Message, int>().AddAsync(message);
            await unitOfWork.SaveChangesAsync();
        }

        //public async Task<List<MessegeDTO>> GetAllMessegesAsync(int chatSessionId)
        //{
        //    var messages = await unitOfWork.GetRepository<Message, int>().GetAllAsync();
        //    var chatMessages = messages.Where(m => m.ChatSessionId == chatSessionId).ToList().OrderByDescending(m => m.SentAt);
        //    if (chatMessages == null || !chatMessages.Any())
        //    {
        //        return new List<MessegeDTO>();
        //    }
        //    var messegeDTOs = mapper.Map<List<MessegeDTO>>(chatMessages);
        //    return messegeDTOs;
        //}


    }
}
