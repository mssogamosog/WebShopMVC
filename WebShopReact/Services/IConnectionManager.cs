using WebShopMVC.Models;

namespace WebShopReact.Managers
{
    public interface IConnectionManager
    {
        string ChangeConnection(InMemory connection);
    }
}