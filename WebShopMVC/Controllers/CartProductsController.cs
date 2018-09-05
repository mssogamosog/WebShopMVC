using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShopMVC.Managers;
using WebShopMVC.Models;

namespace WebShopMVC.Controllers
{
    public class CartProductsController : Controller
    {
        ICartProductsManager _cartProductsManager;

        public CartProductsController(ICartProductsManager cartProductsManager)
        {
            _cartProductsManager = cartProductsManager;
        }

        public IActionResult AddToCart([Bind("ProductId,Name,Description,Price,Quantity")] Product product)
		{
            _cartProductsManager.AddToCart(product);

            return RedirectToAction("Details", "Products", new { id = product.ProductId });
		}
	
	// GET: CartProducts
	public  IActionResult Index()
        {
            var productsInCart = _cartProductsManager.GetProducts();
            return View( productsInCart);
        }


        // GET: CartProducts/Delete/5
        public async Task<IActionResult> Delete(int ProductId,int CartId)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var cartProducts = _cartProductsManager.Delete(ProductId, CartId);
            if (cartProducts == null)
            {
                return NotFound();
            }

            return View(cartProducts);
        }

        // POST: CartProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
