using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CoreEntites.EmergencyEntities;

namespace Domain.Entities.CoreEntites.CarMaintenance_Entities
{
    public class Car : BaseEntity<int>
    {
        #region Owner
        public int OwnerId { get; set; }
        public CarOwner Owner { get; set; }
        #endregion

        #region Data
        public string Vendor {  get; set; }
        public string ModelName { get; set; }
        public string TypeOfCar { get; set; }
        public string TypeOfFuel { get; set; }
        public int modelYear { get; set; }
        public int AvgKmPerMonth { get; set; }
        public decimal TotalMaintenanceCost { get; set; }
        public int MaintenanceCount { get; set; }
        #endregion

        public ICollection<CarMaintenanceRecord> CarMaintenanceRecords { get; set; } = new HashSet<CarMaintenanceRecord>();
    }
}
