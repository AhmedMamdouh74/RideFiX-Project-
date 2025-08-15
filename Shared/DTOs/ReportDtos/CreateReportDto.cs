using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.ReportDtos
{
    public class CreateReportDto
    {
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ReportingUserId { get; set; }
        public string? ReportedUserId { get; set; }
        public int RequestId { get; set; }
    }
}
