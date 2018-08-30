﻿using System;
using System.Collections.Generic;

namespace WebShopMVC.Models
{
    public partial class Product
    {
        public Product()
        {
            CartProducts = new HashSet<CartProducts>();
            ProductCategory = new HashSet<ProductCategory>();
            ProductOrder = new HashSet<ProductOrder>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public double? Price { get; set; }
        public int? OrderId { get; set; }

        public ICollection<CartProducts> CartProducts { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
        public ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
