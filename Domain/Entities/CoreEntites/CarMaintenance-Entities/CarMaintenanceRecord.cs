using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.CarMaintenance_Entities
{
    public class CarMaintenanceRecord : BaseEntity<int>
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int MaintenanceTypeId { get; set; }
        public MaintenanceTypes MaintenanceType { get; set; }
        public DateTime PerformedAt { get; set; }  
        public DateTime NextMaintenanceDue {  get; set; }
        public int CarKMsAtTime { get; set; }
        public string MaintenanceCenter { get; set; }
        public decimal Cost { get; set; }
        public string? Comment { get; set; }
    }
}
