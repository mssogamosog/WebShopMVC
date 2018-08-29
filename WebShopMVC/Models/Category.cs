using System;
using System.Collections.Generic;

namespace WebShopMVC.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public string Description { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
