using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;


namespace Advantage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {

        private readonly Apicontext _apiContext;

        public CustomerController(Apicontext context)
        {
             _apiContext = context;
        }
        // GET: api/<Customer>
        [HttpGet]
        public IActionResult Get()
        {
            var data = _apiContext.Customers.OrderBy(c => c.Id);
            return Ok(data);
        }

        // GET api/Customer/5
        [HttpGet("{id}",Name ="GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _apiContext.Customers.Find(id);
            return Ok(customer);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            _apiContext.Customers.Add(customer);
            _apiContext.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
