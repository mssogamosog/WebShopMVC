using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebShopMVC.Models
{
	public interface IWebShopDBContext
	{
		DbSet<Category> Category { get; set; }
		DbSet<Customer> Customer { get; set; }
		DbSet<Order> Order { get; set; }
		DbSet<Product> Product { get; set; }
		DbSet<ProductCategory> ProductCategory { get; set; }
		DbSet<ProductOrder> ProductOrder { get; set; }

		int SaveChanges();
	}
}