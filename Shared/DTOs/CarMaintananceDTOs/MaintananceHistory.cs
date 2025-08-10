using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.CarMaintananceDTOs
{
    public class MaintananceHistory
    {
        public DateTime PerformedAt { get; set; }
        public DateTime NextMaintenanceDue { get; set; }
        public int CarKMsAtTime { get; set; }
        public string MaintenanceCenter { get; set; }
        public string? Comment { get; set; }
        public decimal Cost { get; set; }
    }
}
