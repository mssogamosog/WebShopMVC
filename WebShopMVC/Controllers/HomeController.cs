﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShopMVC.Models;

namespace WebShopMVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult ChangeDatabase()
		{
			if (PublicContext._InMemory == InMemory.WebShopDBContext)
			{
				PublicContext._InMemory = InMemory.WebShopDBContextInMemory;
			}
			else
			{
				PublicContext._InMemory = InMemory.WebShopDBContext;
			}

			return RedirectToAction("Index","Products");
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
