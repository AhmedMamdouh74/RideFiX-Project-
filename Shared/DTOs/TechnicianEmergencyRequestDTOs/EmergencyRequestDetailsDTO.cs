using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.TechnicianEmergencyRequestDTOs
{
    public class EmergencyRequestDetailsDTO
    {
        public bool IsCompleted { get; set; }
        public int TechnicianId { get; set; }
        public int RequestId { get; set; }
        public string Description { get; set; }
        public string CarOwnerName {  get; set; }
        public string FaceImageUrl {  get; set; }

        // From EmergencyRequestTechnicians (technician-specific state)
       public RequestState? RequestState { get; set; }
    }
}
