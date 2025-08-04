using Domain.Entities.CoreEntites.CarMaintenance_Entities;
using Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
  public  class CarOwner : BaseEntity<int>
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } 
        public ICollection<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<EmergencyRequest> EmergencyRequests { get; set; } = new List<EmergencyRequest>();

        public Car? Car { get; set; }

    }
}
