using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopMVC.Models
{

    public enum InMemory
	{
		WebShopDBContextInMemory = 1,
		WebShopDBContext = 2
	}
}
