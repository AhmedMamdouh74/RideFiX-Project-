using Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.e_Commerce
{
    public class Rate : BaseEntity<int>
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Product Product { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
