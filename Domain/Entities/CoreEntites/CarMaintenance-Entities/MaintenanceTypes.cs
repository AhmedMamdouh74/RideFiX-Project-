using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CoreEntites.CarMaintenance_Entities
{
    public class MaintenanceTypes : BaseEntity<int>
    {
        public string Name { get; set; }
        public int? RepeatEveryKM { get; set; }
        public int? RepeatEveryDays { get; set; }
        public ICollection<CarMaintenanceRecord> CarMaintenanceRecords { get; set; } = new HashSet<CarMaintenanceRecord>();
    }
}
