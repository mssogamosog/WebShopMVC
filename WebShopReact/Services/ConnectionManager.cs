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
       // private readonly IContextHelper _contextHelper;
        IMapper _mapper;

        public ConnectionManager(IHttpContextAccessor accessor, ITokenHelper tokenHelper, ICustomerManager customerManager, IMapper mapper)
        {
            _accessor = accessor;
            _tokenHelper = tokenHelper;
            _customerManager = customerManager;
            _mapper = mapper;
        }

        public string SwitchConnection(string connection)
        {
            var id = Int32.Parse(_accessor.HttpContext.User.Identity.Name);
            var currentCustomer = _customerManager.GetCustomer(id);
            var identity = _accessor.HttpContext.User.Identity as ClaimsIdentity; ;
            identity.RemoveClaim(identity.FindFirst("Connection"));
            identity.AddClaim(new Claim("Connection", connection));
           // _context = _contextHelper.SetContext();
            if (currentCustomer != null && _customerManager.GetCustomer(id) == null)
            {
                var customer = _mapper.Map<CustomerDTO>(currentCustomer);
                _customerManager.AddCustomer(customer);
            }
            var connectionType = (InMemory)System.Enum.Parse(typeof(InMemory), connection);
            var token = _tokenHelper.GetToken(currentCustomer);
            return token;
        }
    }
}