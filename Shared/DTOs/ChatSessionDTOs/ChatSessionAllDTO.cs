using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.ChatSessionDTOs
{
    public class ChatSessionAllDTO
    {
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsClosed { get; set; }
        public int CarOwnerId { get; set; }
        public int TechnicianId { get; set; }
    }
}
