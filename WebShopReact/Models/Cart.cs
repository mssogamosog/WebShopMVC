using System;
using System.Collections.Generic;

namespace WebShopMVC.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartProducts = new HashSet<CartProducts>();
            Customer = new HashSet<Customer>();
        }

        public int CartId { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; }
        public ICollection<Customer> Customer { get; set; }
    }
}
