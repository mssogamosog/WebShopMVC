using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopMVC.Models
{
	public static class PublicContext
	{
		public static bool InMemory = false;
		public static void ChangeInMemory()
		{
			InMemory = !InMemory;
		}
		public static readonly IWebShopDBContext _context2 =
			new WebShopDBContext(
			new DbContextOptionsBuilder<WebShopDBContext>()
				.UseInMemoryDatabase()
				.UseInternalServiceProvider(new ServiceCollection()
				.AddEntityFrameworkInMemoryDatabase()
				.BuildServiceProvider())
				.Options);
	}
}
