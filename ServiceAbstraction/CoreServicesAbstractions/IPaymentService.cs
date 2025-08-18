using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedData.DTOs.Payment;

namespace ServiceAbstraction.CoreServicesAbstractions
{
    public interface IPaymentService
    {
        public Task<RideCoinsPaymentDto> CreateOrUpdatePaymentIntentRideCoinsAsync(int CoinChargeId);
        public Task<int> CreateChargeEntityAsync(int Coins);
    }
}
