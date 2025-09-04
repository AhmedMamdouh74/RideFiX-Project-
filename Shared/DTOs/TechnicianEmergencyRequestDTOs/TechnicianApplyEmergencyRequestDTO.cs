using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianEmergencyRequestDTOs
{
    public class TechnicianApplyEmergencyRequestDTO
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public int Pin { get; set; }
    }
}
