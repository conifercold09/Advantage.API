using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Data;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Advantage.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvantageAPIController : ControllerBase
    {
        private Apicontext _apiContext;

        //public AdvantageAPIController(Apicontext apicontroller)
        //{
        //    apicontroller = _apiContext;
        //}

        //private IEnumerable<Apicontext> getCustomer(DataSeed customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _apiContext.Customers.Add();
        //    }
        //} 

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AdvantageAPIController> _logger;

        public AdvantageAPIController(ILogger<AdvantageAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
