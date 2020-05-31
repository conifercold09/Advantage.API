using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly Apicontext _apicontext;
        public OrderController(Apicontext context)
        {
            _apicontext = context;
        }
        //Get api/order/pageNumber/pagesize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _apicontext.Orders.Include(
                o => o.Customer).OrderByDescending(c => c.Placed);
            var page = new paginatedResponse(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPage = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPage
             };


            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _apicontext.Orders.Include(c => c.Customer)
                .ToList();
            var groupResult = orders.GroupBy(o => o.Customer.State)
                .ToList().Select(grp => new
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(res =>res.Total).ToList();
           
            return Ok(groupResult);
        }

        [HttpGet("ByCustomer/{n}")]
        public IActionResult ByCustomer(int n)
        {
            var orders = _apicontext.Orders.Include(c => c.Customer)
                .ToList();
            var groupResult = orders.GroupBy(o => o.Customer.Id)
                .ToList().Select(grp => new
                {
                    Name = _apicontext.Customers.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(res => res.Total).Take(n).ToList();

            return Ok(groupResult);
        }

        [HttpGet("GetOrder/{id}")]
        public IActionResult GetOrder(int id)
        {
            var orders = _apicontext.Orders.Include(c => c.Customer)
                .First(o => o.Id == id);

            return Ok(orders);
        }
    }
}