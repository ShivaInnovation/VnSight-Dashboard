using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VnSight.API.Models;

namespace VnSight.API.Controllers
{

    [Route("api/{controller}")]
    public class ServerController : Controller
    {
        private readonly ApiContext _ctx;

        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Servers.OrderBy(s => s.Id).ToList();
            return Ok(response);
        }

        [HttpGet("{id}", Name="GetServer")]
        public IActionResult Get(int id)
        {
            var response = _ctx.Servers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = _ctx.Servers.Find(id);
            if(server == null)
            {
                return NotFound();
            }
            
            if(msg.Payload == "activate")
            {
                server.IsOnline = true;                
            }

             if(msg.Payload == "deactivate")
            {
                server.IsOnline = false;               
            }

            _ctx.SaveChanges();
            return new NoContentResult();

        }
    }
}