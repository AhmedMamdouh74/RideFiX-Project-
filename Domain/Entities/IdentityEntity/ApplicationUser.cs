using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.CoreEntites.EmergencyEntities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string SSN { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string IdentityImageUrl { get; set; }
        public string FaceImageUrl { get; set; }
        public int PIN { get; set; }
        public bool IsActivated { get; set; }=true;
        public bool isDeleted {  get; set; }=false;



        public ICollection<Message> messages { get; set; } = new HashSet<Message>();
        public ICollection<UserConnectionIds> connections { get; set; } = new HashSet<UserConnectionIds>();
    }
}
