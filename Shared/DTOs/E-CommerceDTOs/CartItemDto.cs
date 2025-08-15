using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData.DTOs.E_CommerceDTOs
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
