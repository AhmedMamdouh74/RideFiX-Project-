using SharedData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.Account
{
    public class ReadUserDetailsDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? FaceImageUrl { get; set; }
        public string? Government { get; set; }
        public List<string>? Category { get; set; }

        public TimeOnly StartWorking { get; set; }
        public TimeOnly EndWorking { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
