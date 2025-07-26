using SharedData.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions.Account
{
    public interface IUserProfileService
    {
        Task<ReadUserDetailsDTO> GetTechnicianDetailsAsync(int userId);
    }
}
