using SharedData.DTOs.MessegeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IMessegeService
    {
        //public Task<List<MessegeDTO>> GetAllMessegesAsync(int chatSessionId);

        public Task AddMessegeAsync(MessegeAllDTO messegeDTO);

    }
}
