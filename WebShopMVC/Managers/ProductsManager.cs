using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebShopMVC.Models;

namespace WebShopMVC.Managers
{
    public class ProductsManager : IProductsManager
    {
        private readonly IIndex<string, IWebShopDBContext> _contexts;
        IWebShopDBContext _context;

        public ProductsManager(IIndex<string, IWebShopDBContext> context)
        {
            _contexts = context;
            SwitchOn();
        }

        void SwitchOn()
        {
            _context = _contexts[PublicContext._InMemory.ToString()];
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
            var product =  _context.Product.FindAsync(id);
            _context.Product.Remove(product.Result);
            _context.SaveChanges();
        }
        public bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
