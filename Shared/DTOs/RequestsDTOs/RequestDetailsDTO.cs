using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.RequestsDTOs
{
    public class RequestDetailsDTO
    {
        public string City { get; set; }
        public string Description { get; set; }
        public string TechnicianName { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string CategoryName { get; set; }
        public DateOnly RequestDate { get; set; }
    }
}
