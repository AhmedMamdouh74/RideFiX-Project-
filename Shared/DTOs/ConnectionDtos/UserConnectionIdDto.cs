using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.ConnectionDtos
{
    public class UserConnectionIdDto
    {
        public string ConnectionId { get; set; } = string.Empty;
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
