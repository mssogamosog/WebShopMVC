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
	public class CartProductsManager : ICartProductsManager
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
            try
            {
                var customer = _context.Customer.Where(c => c.CustomerId == 1).FirstOrDefault();

                var webShopDBContext = _context.CartProducts
                    .Include(c => c.Cart)
                    .Include(c => c.Product)
                    .Where(c => c.CartId == customer.CartId)
                    .ToListAsync();
                return webShopDBContext;
            }
            catch (Exception)
            {

                throw;
            }
			
		}
        public void DeleteConfirmed(int CartId, int ProductId)
        {
            var cartProducts = _context.CartProducts
                .Include(c => c.Product)
                .Where(c => c.CartId == CartId && c.ProductId == ProductId)
                .FirstOrDefault();
            try
            {
                _context.CartProducts.Remove(cartProducts);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public CartProducts Delete(int ProductId, int CartId)
        {
            try
            {
                var cartProducts = _context.CartProducts
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefault(m => m.ProductId == ProductId && m.CartId == CartId);
                return cartProducts;
            }
            catch (Exception)
            {

                return null;
            }
            
            
        }

        private bool CartProductsExists(int id)
        {
            return _context.CartProducts.Any(e => e.ProductId == id);
        }
    }
}
