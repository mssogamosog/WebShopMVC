using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebShopMVC.Models;
using WebShopReact.Models;
namespace WebShopReact.Helpers
{
	public interface ITokenHelper
	{
        string GetToken(Customer customer, InMemory connection = InMemory.WebShopDBContextInMemory);

    }
	public class TokenHelper : ITokenHelper
	{
		private readonly IConfiguration _configuration;
		public TokenHelper(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string GetToken(Customer customer, InMemory connection = InMemory.WebShopDBContextInMemory)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes((string)(_configuration.GetValue(typeof(string), "Key")));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.CustomerId.ToString()),
                    new Claim("Connection",connection.ToString())
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);
			return tokenString;
		}
	}
}