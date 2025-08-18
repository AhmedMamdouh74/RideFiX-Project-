using Domain.Entities.PaymentEntites;
using SharedData.DTOs.Credit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.credit
{
    public class CoinTopUp : BaseEntity<int>
    {

        public string UserId { get; set; } = default!;
        public int Coins { get; set; }
        public long AmountCents { get; set; }
        public string Currency { get; set; } = "usd";
        public CoinChargeEntity coinChargeEntity { get; set; } = default!;
        public TopUpStatus Status { get; set; } = TopUpStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; } 
        
    }
}
