using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.E_CommerceDTOs
{
    public class ProductWithRatesDTO : ProductBreifDTO
    {
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public List<RateDTO> ProductRates { get; set; } = new List<RateDTO>();
    }
}
