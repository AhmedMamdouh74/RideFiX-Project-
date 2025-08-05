using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.MessegeDTOs
{
    public class MessegeAllDTO
    {
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }

        public int ChatSessionId { get; set; }
        public string ApplicationId { get; set; }

    }
}
