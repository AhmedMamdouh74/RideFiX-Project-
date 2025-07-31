using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class Review : BaseEntity<int>
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey ("CarOwner")]
        public int CarOwnerId { get; set; }
        public CarOwner CarOwner { get; set; }

        public EmergencyRequest EmergencyRequest { get; set; }
        [ForeignKey("EmergencyRequest")]
        public int? EmergencyRequestId { get; set; }



        [ForeignKey("Technician")]
        public int TechnicianId { get; set; }
        public Technician Technician { get; set; }
    }
}
