using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.MessegeDTOs
{
    public class MessegeDTO
    {
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public string ApplicationId { get; set; }

    }
}
