using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.CarMaintananceDTOs
{
    public class ScheduleDTO
    {
        public string ToEmail { get; set; }
        public string MaintananceType { get; set; }
        public string Ownername { get; set; }
        public DateOnly MaintananceDate { get; set; }
    }
}
