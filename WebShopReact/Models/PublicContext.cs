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
		public static InMemory _InMemory = InMemory.WebShopDBContextInMemory;
	}
}
