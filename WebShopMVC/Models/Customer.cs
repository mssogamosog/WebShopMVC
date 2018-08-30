using System;
using System.Collections.Generic;

namespace WebShopMVC.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string ContactInfo { get; set; }
        public int? CartId { get; set; }

        public Cart Cart { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
