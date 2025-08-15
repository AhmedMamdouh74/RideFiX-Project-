using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.CarMaintananceDTOs
{
    public class CarMaintananceAllDTO
    {
        public int? CarId { get; set; }
        public int MaintenanceTypeId { get; set; }
        public DateTime PerformedAt { get; set; }
        public int CarKMsAtTime { get; set; }
        public string MaintenanceCenter { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
    }
}
