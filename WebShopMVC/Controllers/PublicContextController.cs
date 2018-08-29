using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShopMVC.Models;

namespace WebShopMVC.Controllers
{
    public class PublicContextController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult ChangeInMemory()
		{
			PublicContext.ChangeInMemory();
			return View("Index");
		}
	}
}