using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
    public class TechReverseRequest : BaseEntity<int>
    {
        [ForeignKey("EmergencyRequest")]
        public int EmergencyRequestId { get; set; }
        public EmergencyRequest EmergencyRequest { get; set; }
        [ForeignKey("Technician")]
        public int TechnicianId { get; set; }
        public Technician Technician { get; set; }
        public DateTime TimeStamp { get; set; }
        public RequestState CallState { get; set; } 
    }
}
