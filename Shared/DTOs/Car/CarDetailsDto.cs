using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.Car
{
    public class CarDetailsDto
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string ModelName { get; set; }
        public string TypeOfCar { get; set; }
        public string TypeOfFuel { get; set; }
        public string ModelYear { get; set; } // نص زي TypeScript
        public int AvgKmPerMonth { get; set; }
        public decimal TotalMaintenanceCost { get; set; }
        public int MaintenanceCount { get; set; }
        public int DaysSinceLastMaintenance { get; set; }
    }
}
