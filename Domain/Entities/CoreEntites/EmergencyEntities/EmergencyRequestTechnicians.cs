using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
    public class EmergencyRequestTechnicians:BaseEntity<int>
    {
        public RequestState CallStatus { get; set; }
        [ForeignKey("Technician")]
        public int TechnicianId { get; set; }
        public Technician Technician { get; set; }
        [ForeignKey("EmergencyRequests")]
        public int EmergencyRequestId { get; set; }
        public EmergencyRequest  EmergencyRequests { get; set; } 
    }
}
