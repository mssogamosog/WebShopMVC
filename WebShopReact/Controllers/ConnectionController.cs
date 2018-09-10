using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShopMVC.Models;
using WebShopReact.Managers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShopReact.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        IConnectionManager _connectionManager;

        public ConnectionController(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        [Route("/[controller]/{connection}")]
        [HttpPost("{connection}")]
        public IActionResult ChangeConnection([FromRoute] string connection)
        {
            var connectionType = (InMemory)System.Enum.Parse(typeof(InMemory), connection);
            return Ok(_connectionManager.ChangeConnection(connectionType));
        }
    }
}
