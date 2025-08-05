using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.MessegeDTOs
{
    public class MessegeMiniDTO
    {     
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public string ApplicationId { get; set; }
        public bool IsSeen { get; set; }
    }
}
