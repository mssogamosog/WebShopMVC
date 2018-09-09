using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopMVC.Models;
using WebShopReact.Helpers;

namespace WebShopMVC.Managers
{
    public class ProductsManager : IProductsManager
    {
        private readonly IIndex<string, IWebShopDBContext> _contexts;
        IHttpContextAccessor _httpContextAccessor;
        IWebShopDBContext _context;
        IConnectionHelper _connectionHelper;

        public ProductsManager(IIndex<string, IWebShopDBContext> contexts, IHttpContextAccessor httpContextAccessor, IConnectionHelper connectionHelper)
        {
            _contexts = contexts;
            _httpContextAccessor = httpContextAccessor;
            _connectionHelper = connectionHelper;
            SwitchOn();
        }

        void SwitchOn()
        {
            _context = _contexts[_connectionHelper.SetContext()];
        }

        public List<Product> GetProducts()
        {
            try
            {
                return _context.Product.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public Product Details(int? id)
        {
            Product product;
            try
            {
                return product = _context.Product
                .FirstOrDefault(m => m.ProductId == id);
            }
            catch (Exception)
            {

                return null;
            }
            
        }
        public void Create(Product product)
        {
            try
            {
                _context.Product.Add(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public Product Edit(int? id)
        {
            Product product;
            try
            {
                return product = _context.Product.Find(id);
            }
            catch (Exception)
            {

                return null;
            }

        }
        public void Edit(Product product)
        {
            try
            {
                _context.Product.Update(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {

               throw;
            }

        }
        public  Product Delete(int? id)
        {
            try
            {
                var product = _context.Product
                .FirstOrDefault(m => m.ProductId == id);
                return product;
            }
            catch (Exception)
            {

                return   null     ;
            }
            
        }
        public void DeleteConfirmed(int id)
        {
            var product =  _context.Product
                .FirstOrDefault(m => m.ProductId == id);
            _context.Product.Remove(product);
            _context.SaveChanges();
        }
        public bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
