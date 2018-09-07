using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopMVC.Managers;
using WebShopMVC.Models;

namespace WebShopReact.Controllers
{
    [Route("/[controller]")]
    [ApiController]
	[Authorize]
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
		[Route("/[controller]/")]
		[HttpGet]
		public  IActionResult Index()
		{
			var productsInCart = _cartProductsManager.GetProducts();
			return Ok(productsInCart);
		}

		// POST: CartProducts/Delete/5
		[Route("/[controller]/{ProductId}/{CartId}")]
		[HttpDelete]
		public async Task<IActionResult> DeleteConfirmed( [FromRoute]int ProductId,[FromRoute] int CartId)
		{
			if (CartProductsExists( ProductId, CartId))
			{
				_cartProductsManager.DeleteConfirmed(CartId, ProductId);
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return NotFound();
			}
			
		}

		private bool CartProductsExists(int ProductId, int CartId)
		{
			return _cartProductsManager.CartProductsExists( ProductId,  CartId);
		}
	}
}