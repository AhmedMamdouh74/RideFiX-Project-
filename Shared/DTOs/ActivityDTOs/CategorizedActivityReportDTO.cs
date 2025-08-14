using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.ActivityDTOs
{
    public class CategorizedActivityReportDTO
    {
        public List<ActivityReportDTO> EmergencyRequests { get; set; } = new List<ActivityReportDTO>();
        public List<ActivityReportDTO> CarMaintenanceRecords { get; set; } = new List<ActivityReportDTO>();
        public List<ActivityReportDTO> UserRegistrations { get; set; } = new List<ActivityReportDTO>();
        public List<ActivityReportDTO> Reviews { get; set; } = new List<ActivityReportDTO>();
        public List<ActivityReportDTO> ChatSessions { get; set; } = new List<ActivityReportDTO>();
        public int TotalCount { get; set; }
        public DateTime ReportGeneratedAt { get; set; } = DateTime.UtcNow;
    }
}

