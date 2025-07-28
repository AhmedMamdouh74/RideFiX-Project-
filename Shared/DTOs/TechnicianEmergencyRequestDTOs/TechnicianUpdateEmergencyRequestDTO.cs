using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianEmergencyRequestDTOs
{
    public class TechnicianUpdateEmergencyRequestDTO
    {
     
        public int TechnicianId {  get; set; }
        public int RequestId { get; set; }
        [Range(1, 2, ErrorMessage = "Status must be 1 (Answered) or 2 (Rejected).")]
        public RequestState RequestState { get; set; } // Accept / Reject
        public int Pin {  get; set; }
    }
}
