using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.RequestsDTOs
{
    public class RealRequestDTO
    {
        
        public HashSet<int> TechnicianIDs { get; set; }
        public string Description { get; set; }
        public HashSet<string>? ImageUrl { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
        public int categoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CarOwnerId must be greater than 0.")]
        public int CarOwnerId { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "PIN is required.")]
        public int pin { get; set; }

    }
}
