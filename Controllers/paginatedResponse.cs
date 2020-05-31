using Advantage.API.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Advantage.API.Controllers
{
    internal class paginatedResponse
    {
         public int Total { get; set; }
        public IEnumerable<object> Data { get; set; }
        public paginatedResponse(IEnumerable<object> data,int i, int len)
        {
            Data = data.Skip((i - 1) * len).Take(len).ToList();
            Total = data.Count();
        }
    }
}