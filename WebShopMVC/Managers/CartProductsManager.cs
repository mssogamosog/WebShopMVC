using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShopMVC.Models;

namespace WebShopMVC.Managers
{
	public class CartProductsManager 
	{
		private readonly IIndex<string, IWebShopDBContext> _contexts;
		IWebShopDBContext _context;

		public CartProductsManager(IIndex<string, IWebShopDBContext> context)
		{
			_contexts = context;
			SwitchOn();
		}

		void SwitchOn()
		{
			_context = _contexts[PublicContext._InMemory.ToString()];
		}
		public void AddToCart(Product product)
		{
			var customer = _context.Customer.Include(c => c.Cart).Where(c => c.CustomerId == 1).FirstOrDefault();
			if (customer.Cart == null)
			{
				var newCart = new Cart();
				customer.Cart = newCart;
			}
			var cartProducts = _context.CartProducts.Where(sc => sc.CartId == customer.Cart.CartId && sc.ProductId == product.ProductId).FirstOrDefault();
			if (cartProducts != null)
			{
				cartProducts.Quantity = product.Quantity;
			}
			else
			{
				cartProducts = new CartProducts
				{
					ProductId = product.ProductId,
					Cart = customer.Cart,
					Quantity = product.Quantity
				};
				customer.Cart.CartProducts.Add(cartProducts);
			}
			_context.SaveChanges();
		}
		public Task<List<CartProducts>> GetProducts()
		{
			var customer = _context.Customer.Where(c => c.CustomerId == 1).FirstOrDefault();

			var webShopDBContext = _context.CartProducts
				.Include(c => c.Cart)
				.Include(c => c.Product)
				.Where(c => c.CartId == customer.CartId)
				.ToListAsync();
			return webShopDBContext;
		}
	}
}
