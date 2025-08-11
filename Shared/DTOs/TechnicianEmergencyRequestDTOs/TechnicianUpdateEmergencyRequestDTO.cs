using SharedData.Enums;
using System.ComponentModel.DataAnnotations;

namespace SharedData.DTOs.TechnicianEmergencyRequestDTOs
{
    public class TechnicianUpdateEmergencyRequestDTO
    {

        public int TechnicianId { get; set; }
        public int RequestId { get; set; }
        [Range(1, 2, ErrorMessage = "Status must be 1 (Answered) or 2 (Rejected).")]
        public RequestState RequestState { get; set; } // Accept / Reject
        [Required(ErrorMessage = "PIN is required")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "PIN must be between 4 and 6 digits")]
        public int Pin { get; set; }
    }
}
