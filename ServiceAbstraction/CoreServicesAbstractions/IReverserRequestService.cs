using SharedData.DTOs.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IReverserRequestService
    {
        public Task<List<NotificationDto>> GetReverserequest();
        public Task AcceptRequest(int requestId);
        public Task RejectRequest(int requestId);

    }
}
