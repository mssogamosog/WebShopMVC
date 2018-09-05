using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopMVC.Managers;
using WebShopMVC.Models;

namespace WebShopReact.Controllers
{
	[Route("/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
    {
		IProductsManager _productsManager;

		public ProductsController(IProductsManager productsManager)
		{
			_productsManager = productsManager;
		}
		[HttpGet]
		public IActionResult Index()
		{
			return Ok(_productsManager.GetProducts());
		}
		[HttpGet("{id}")]
		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = _productsManager.Details(id);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		// GET: Products/Create

		// POST: Products/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public IActionResult Create([FromBody] Product product)
		{
			if (ModelState.IsValid)
			{
				_productsManager.Create(product);
				return CreatedAtAction("Details", new { id = product.ProductId }, product);
			}
			else
			{
				return BadRequest(ModelState);
			}
			
		}
		// POST: Products/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPut("{id}")]
		public async Task<IActionResult> Edit(int id, [FromBody] Product product)
		{
			if (!ProductExists(id))
			{
				return NotFound();
			}
			else
			{
				product.ProductId = id;
				if (ModelState.IsValid)
				{
					try
					{
						_productsManager.Edit(product);
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!ProductExists(product.ProductId))
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
				return Ok(product);
			}
			
		}
		

		// Delete: Products/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			_productsManager.DeleteConfirmed(id);
			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(int id)
		{
			return _productsManager.ProductExists(id);

		}
	}
}