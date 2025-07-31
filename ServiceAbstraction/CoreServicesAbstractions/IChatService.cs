using SharedData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IChatService
    {
        public Task<List<ChatBreifDTO>> GetAllChatsAsync();
    }
}
