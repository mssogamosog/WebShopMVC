using System;
using System.Collections.Generic;

namespace WebShopMVC.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrder = new HashSet<ProductOrder>();
        }

        public int OrderId { get; set; }
        public int? Quantity { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
