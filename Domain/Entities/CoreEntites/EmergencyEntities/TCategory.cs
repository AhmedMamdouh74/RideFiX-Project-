using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class TCategory : BaseEntity<int>
    {
        public string Name { get; set; } // name of category 

        public string Description { get; set; }
        public bool IsDeleted { get; set; }=false;

        public ICollection<Technician> Technicians { get; set; } = new HashSet<Technician>(); // List OF Technicains
        public ICollection<EmergencyRequest> EmergencyRequests { get; set; } = new HashSet<EmergencyRequest>();
    }
}
