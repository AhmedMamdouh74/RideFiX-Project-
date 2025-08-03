using SharedData.DTOs.MessegeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.ChatDTOs
{
    public class ChatDetailsDTO
    {
        public string name { get; set; }
        public string imgurl { get; set; }
        public bool IsClosed { get; set; }
        public DateTime EndAt { get; set; }

        public List<MessegeDTO>? messages { get; set; } = new List<MessegeDTO>();

    }
}