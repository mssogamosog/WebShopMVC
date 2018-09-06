using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Managers;
using WebShopMVC.Models;
using WebShopReact.Models;

namespace WebShop.Controllers
{
	[Route("/[controller]")]
	[ApiController]
	public class CustomersController : Controller
	{
		private readonly ICustomerManager _customerManager;

		public CustomersController(ICustomerManager customerManager)
		{
			_customerManager = customerManager;
		}

		// POST: Customers/authenticate
		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]CustomerDTO customer)
		{
			var response = _customerManager.Authenticate(customer);
			if (response == null) return BadRequest("Email or password is incorrect");
			return Ok(response);
		}

		// GET: Customers/Details/5
		[HttpGet("{id}")]
		[Authorize]
		public IActionResult Details(int? id)
		{
			var customer = _customerManager.GetCustomer((int)id);
			if (customer == null)
			{
				return NotFound();
			}
			return Ok(customer);
		}

		// POST: Customers/Create
		[HttpPost]
		public IActionResult Create([FromBody] CustomerDTO customer)
		{
			if (ModelState.IsValid)
			{
				_customerManager.AddCustomer(customer);
				return CreatedAtAction("Details", new { id = customer.CustomerId });
			}
			return BadRequest(ModelState);
		}
		// POST: Customers/Edit/5
		[HttpPut("{id}")]
		public IActionResult Edit(int id, [FromBody] Customer customer)
		{
			if (id != customer.CustomerId)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				_customerManager.UpdateCustomer(customer);
				return Ok();
			}
			return BadRequest(ModelState);
		}
	}
}