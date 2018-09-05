using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopMVC.Managers;
using WebShopMVC.Models;

namespace WebShopReact.Controllers
{
    [Route("/[controller]")]
    [ApiController]
	public class CartProductsController : ControllerBase
	{
		ICartProductsManager _cartProductsManager;

		public CartProductsController(ICartProductsManager cartProductsManager)
		{
			_cartProductsManager = cartProductsManager;
		}
		[HttpPost]
		public IActionResult AddToCart([Bind("ProductId,Name,Description,Price,Quantity")] Product product)
		{
			_cartProductsManager.AddToCart(product);

			return RedirectToAction("Details", "Products", new { id = product.ProductId });
		}

		// GET: CartProducts
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var productsInCart = _cartProductsManager.GetProducts();
			return Ok(await productsInCart);
		}

		// POST: CartProducts/Delete/5
		[HttpDelete]
		public async Task<IActionResult> DeleteConfirmed(int CartId, int ProductId)
		{
			_cartProductsManager.DeleteConfirmed(CartId, ProductId);
			return RedirectToAction(nameof(Index));
		}

		private bool CartProductsExists(int id)
		{
			return CartProductsExists(id);
		}
	}
}