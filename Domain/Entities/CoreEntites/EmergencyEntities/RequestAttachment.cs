using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.EmergencyEntities
{
    public class RequestAttachment : BaseEntity<int>
    {
        public string AttachmentUrl { get; set; }

        [ForeignKey("EmergencyRequest")]
        public int EmergencyRequestId { get; set; }
        // Navigational Prop
        public EmergencyRequest EmergencyRequest { get; set; }
    }
}
