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
        public int CarOwnerId { get; set; }
        
        public string? Description { get; set; }
        public string? CarOwnerName {  get; set; }
        public string? FaceImageUrl {  get; set; }
        public string? RequestImageUrl {  get; set; }

        // From EmergencyRequestTechnicians (technician-specific state)
        public RequestState? RequestState { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime? EndTimeStamp { get; set; }
        public string? Category {  get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public string? AttachmentUrl { get; set; }
    }
}
