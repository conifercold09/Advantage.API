using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[Controller]")]
    public class ServerController : Controller
    {
        private readonly Apicontext _apicontext;
        public ServerController(Apicontext context)
        {
            _apicontext = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _apicontext.Servers.OrderBy(s => s.Id).ToList();
            return Ok(response);
        }
        [HttpGet("{id}",Name ="GetServer")]
        public IActionResult Get(int id)
        {
            var response = _apicontext.Servers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = _apicontext.Servers.Find(id);
            if (server == null)
            {
                return NotFound();
            }
            if (msg.PayLoad =="activate")
            {
                server.IsOnline = true;
           }
            if (msg.PayLoad == "deactivate")
            {
                server.IsOnline = false;
            }
            _apicontext.SaveChanges();

            return new NoContentResult();
        }


    }
}