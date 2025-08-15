using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.ActivityDTOs
{
    public class ActivityReportDTO
    {
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string TimeAgo { get; set; }
        public string EntityType { get; set; }
        public int EntityId { get; set; }
    }

    public class ActivityReportResponseDTO
    {
        public List<ActivityReportDTO> Activities { get; set; } = new List<ActivityReportDTO>();
        public int TotalCount { get; set; }
        public DateTime ReportGeneratedAt { get; set; } = DateTime.UtcNow;
    }
}

