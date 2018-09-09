using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopMVC.Models;

namespace WebShopReact.Helpers
{
    public class ConnectionHelper : IConnectionHelper
    {

        private readonly IHttpContextAccessor _accessor;

        public ConnectionHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string SetContext()
        {
            var userClaim = _accessor.HttpContext.User.Claims.Where(c => c.Type == "Connection").FirstOrDefault();
            var connection = InMemory.WebShopDBContextInMemory.ToString();
            if (userClaim != null)
            {
                connection = userClaim.Value;
                return connection;
            }
            else
            {
                return connection;
            }            
        }
    }
}
