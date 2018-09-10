using Autofac.Features.Indexed;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using WebShop.Managers;
using WebShopMVC.Models;
using WebShopReact.Helpers;
using WebShopReact.Models;

namespace WebShopReact.Managers
{
    public class ConnectionManager :  IConnectionManager
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICustomerManager _customerManager;
        private readonly IConnectionHelper _connectionHelper;
        IMapper _mapper;
        IWebShopDBContext _context;
        IIndex<string, IWebShopDBContext> _contexts;

        public ConnectionManager(IHttpContextAccessor accessor, ITokenHelper tokenHelper, ICustomerManager customerManager, IMapper mapper, IConnectionHelper connectiontHelper, IIndex<string, IWebShopDBContext> contexts)
        {
            _contexts = contexts;
            _accessor = accessor;
            _tokenHelper = tokenHelper;
            _customerManager = customerManager;
            _mapper = mapper;
            _connectionHelper = connectiontHelper;
        }

        public string ChangeConnection(InMemory connection)
        {
            var id = Int32.Parse(_accessor.HttpContext.User.Identity.Name);
            var currentCustomer = _customerManager.GetCustomer(id);
            var identity = _accessor.HttpContext.User.Identity as ClaimsIdentity; ;
            identity.RemoveClaim(identity.FindFirst("Connection"));
            identity.AddClaim(new Claim("Connection", connection.ToString()));
            _context = _contexts[_connectionHelper.SetContext()];
            if (currentCustomer != null && _context.Customer.Where(c => c.CustomerId == id).FirstOrDefault() == null)
            {
                _context.Customer.Add(currentCustomer);
            }
            var token = _tokenHelper.GetToken(currentCustomer,connection);
            return token;
        }
    }
}