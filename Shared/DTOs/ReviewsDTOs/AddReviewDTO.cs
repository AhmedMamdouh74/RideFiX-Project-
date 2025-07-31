using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.ReviewsDTOs
{
    public class AddReviewDTO
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int CarOwnerId { get; set; }
        public int? TechnicianId { get; set; }
        public int RequestId { get; set; }
    }
}
