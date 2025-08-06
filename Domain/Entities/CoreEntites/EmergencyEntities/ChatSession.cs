using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class ChatSession : BaseEntity<int>
   {
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<Message> massages { get; set; } = new List<Message>();

        [ForeignKey("CarOwner")]
        public int CarOwnerId { get; set; }
        public CarOwner CarOwner { get; set; }
        [ForeignKey("Technician")]
        public int TechnicianId { get; set; }
        public Technician Technician { get; set; }
    }
}
