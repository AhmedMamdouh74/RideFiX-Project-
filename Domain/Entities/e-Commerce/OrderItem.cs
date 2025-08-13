using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.e_Commerce
{
    public class OrderItem : BaseEntity<int>
    {
        #region Order Nav
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        #endregion

        #region Product Nav
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        #endregion

        public int Quantity { get; set; }

        public decimal totalPrice { get; set; }
    }
}
