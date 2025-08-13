using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.e_Commerce
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        // Navigation property for products in this category
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
