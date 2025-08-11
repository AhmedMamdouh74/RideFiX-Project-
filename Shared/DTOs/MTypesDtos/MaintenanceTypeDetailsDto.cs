using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.MTypesDtos
{
    public class MaintenanceTypeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RepeatEveryKM { get; set; }
        public int? RepeatEveryDays { get; set; }
    }
}
