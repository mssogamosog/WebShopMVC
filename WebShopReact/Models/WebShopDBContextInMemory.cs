﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebShopMVC.Models
{
    public partial class WebShopDBContextInMemory : WebShopDBContext, IWebShopDBContext
	{
        public WebShopDBContextInMemory()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

				optionsBuilder.UseInMemoryDatabase("InMemDb").ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
			}
        }
		

    }
}
