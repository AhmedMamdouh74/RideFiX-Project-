using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions.CarMservices
{
    public interface IEmailService
    {
        public Task SendEmail(string toEmail, string maintananceType, string ownername, DateOnly maintananceDate);


    }
}
