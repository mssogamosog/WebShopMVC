﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopMVC.Models;

namespace WebShopMVC.Managers
{
    public interface ICartProductsManager
    {
        void AddToCart(Product product);
        CartProducts Delete(int ProductId, int CartId);
        void DeleteConfirmed(int CartId, int ProductId);
        Task<List<CartProducts>> GetProducts();
    }
}