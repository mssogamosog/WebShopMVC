using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebShopMVC.Models
{
    public partial class CartProducts
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int? Quantity { get; set; }
		[JsonIgnore]
		public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
