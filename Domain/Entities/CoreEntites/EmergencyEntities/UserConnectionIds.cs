using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.IdentityEntities;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
    public class UserConnectionIds
    {
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ConnectionId { get; set; }
    }
}
