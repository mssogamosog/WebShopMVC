using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopMVC.Models;

namespace WebShopMVC.Managers
{
    public interface IProductsManager
    {
        void Create(Product product);
        Product Delete(int? id);
        void DeleteConfirmed(int id);
        Product Details(int? id);
        Product Edit(int? id);
        void Edit(Product product);
        List<Product> GetProducts();
        bool ProductExists(int id);
    }
}