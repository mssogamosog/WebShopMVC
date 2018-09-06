using WebShopMVC.Models;
using WebShopReact.Models;

namespace WebShop.Managers
{
	public interface ICustomerManager
	{
		void AddCustomer(CustomerDTO customerIn);
		object Authenticate(CustomerDTO customerIn);
		Customer GetCustomer(int id);
		void UpdateCustomer(Customer customer);
	}
}