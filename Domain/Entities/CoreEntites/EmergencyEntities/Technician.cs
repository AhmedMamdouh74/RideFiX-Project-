using Domain.Entities.IdentityEntities;
using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
   public class Technician : BaseEntity<int>
    {
       
        public TimeOnly StartWorking { get; set; }
        public TimeOnly EndWorking { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
        public ICollection<TCategory> TCategories { get; set; } = new List<TCategory>();
        public ICollection<Review> reviews { get; set; } = new List<Review>();
        public ICollection<EmergencyRequest> EmergencyRequests { get; set; } = new List<EmergencyRequest>();
        public ICollection<EmergencyRequestTechnicians> EmergencyRequestTechnicians { get; set; } =  new HashSet<EmergencyRequestTechnicians>();
        public ICollection<TechReverseRequest> TechReverseRequests { get; set; } = new List<TechReverseRequest>();
        public Government government { get; set; }
    }
}
