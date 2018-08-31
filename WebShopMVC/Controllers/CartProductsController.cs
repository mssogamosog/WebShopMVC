using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShopMVC.Models;

namespace WebShopMVC.Controllers
{
    public class CartProductsController : Controller
    {
		private readonly IIndex<string, IWebShopDBContext> _contexts;
		IWebShopDBContext _context;

		public CartProductsController(IIndex<string, IWebShopDBContext> context)
		{
			_contexts = context;
			SwitchOn();
		}

		void SwitchOn()
		{
			_context = _contexts[PublicContext._InMemory.ToString()];
		}

		public IActionResult AddToCart([Bind("ProductId,Name,Description,Price,Quantity")] Product product)
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
			return RedirectToAction("Details", "Products", new { id = product.ProductId });
		}
	
	// GET: CartProducts
	public async Task<IActionResult> Index()
        {
			var customer = _context.Customer.Where(c => c.CustomerId == 1).FirstOrDefault();

			var webShopDBContext = _context.CartProducts
				.Include(c => c.Cart)
				.Include(c => c.Product)
				.Where(c => c.CartId == customer.CartId);
            return View(await webShopDBContext.ToListAsync());
        }

		// GET: CartProducts/Details/5
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Details(int id)
        {
         
            var cartProducts = await _context.CartProducts
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (cartProducts == null)
            {
                return NotFound();
            }

            return View(cartProducts);
        }

        // GET: CartProducts/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            return View();
        }

        // POST: CartProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CartId,Quantity")] CartProducts cartProducts)
        {
            if (ModelState.IsValid)
            {
                _context.CartProducts.Add(cartProducts);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartProducts.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", cartProducts.ProductId);
            return View(cartProducts);
        }

        // GET: CartProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartProducts = await _context.CartProducts.FindAsync(id);
            if (cartProducts == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartProducts.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", cartProducts.ProductId);
            return View(cartProducts);
        }

        // POST: CartProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CartId,Quantity")] CartProducts cartProducts)
        {
            if (id != cartProducts.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.CartProducts.Update(cartProducts);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartProductsExists(cartProducts.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "CartId", cartProducts.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", cartProducts.ProductId);
            return View(cartProducts);
        }

        // GET: CartProducts/Delete/5
        public async Task<IActionResult> Delete(int ProductId,int CartId)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var cartProducts = await _context.CartProducts
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.ProductId == ProductId && m.CartId == CartId);
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
			var cartProducts = _context.CartProducts
				.Include(c => c.Product)
				.Where(c => c.CartId == CartId && c.ProductId == ProductId)
				.FirstOrDefault();
            _context.CartProducts.Remove(cartProducts);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CartProductsExists(int id)
        {
            return _context.CartProducts.Any(e => e.ProductId == id);
        }
    }
}
