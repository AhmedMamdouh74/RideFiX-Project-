using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.Car
{
    public class CreateCarDto
    {
        public string Vendor { get; set; }
        public string ModelName { get; set; }
        public string TypeOfCar { get; set; }
        public string TypeOfFuel { get; set; }
        public string ModelYear { get; set; }
        public int AvgKmPerMonth { get; set; }
    }


    }
