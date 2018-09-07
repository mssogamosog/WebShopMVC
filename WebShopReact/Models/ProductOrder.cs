using System;
using System.Collections.Generic;

namespace WebShopMVC.Models
{
    public partial class ProductOrder
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
