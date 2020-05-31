using Advantage.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Data
{
    
    public class DataSeed
    {
        private readonly Apicontext _ctx;
        public DataSeed(Apicontext ctx)
        {
            this._ctx = ctx;
        }

        public void SeedData(int nCustomers, int nOrders)
        {
            if (!_ctx.Customers.Any())
            {
                SeedCustomers(nCustomers);
                _ctx.SaveChanges();
            }
            if (!_ctx.Orders.Any())
            {
                SeedOrderss(nOrders);
                _ctx.SaveChanges();
            }
            if (!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }
        }

        private void SeedCustomers(int nCustomers)
        {
            List<Customer> customers = BuildCustomerList(nCustomers);
            foreach (var customer in customers)
            {
                _ctx.Customers.Add(customer);
            }
        }

        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customers = new List<Customer>();
            var names = new List<string>();
            for (int i = 0; i < nCustomers; i++)
            {
                var name = Helper.MakeUniqueCustomerName(names);
                names.Add(name);
                var email = Helper.MakeEmail(name);
                customers.Add(new Customer
                {
                    Name = name,
                    Email = email.ToString(),
                    State = Helper.GetRandomState()

                }); ;
            }

            return customers;
        }

        private void SeedOrderss(int nOrders)
        {
            List<Order> OrdersList = BuildOrderList(nOrders);
            foreach (var orders in OrdersList)
            {
                _ctx.Orders.Add(orders);
            }
        }

        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();
            var rand = new Random();
            for (int i = 0; i < nOrders; i++)
            {
                var randCustomerId = rand.Next(_ctx.Customers.Count());
                if (randCustomerId == 0 || randCustomerId > 10 )
                {
                    randCustomerId = 1;
                }
                var placed = Helper.GetRandomOrderPlaced();
                var completed = Helper.GetRandomOrderCompleted(placed);
                orders.Add(new Order
                {
                    
                    Customer = _ctx.Customers.First(c => c.Id == randCustomerId),
                    Total = Helper.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                }); ;
            }

            return orders;
        }

        private void SeedServers()
        {
            List<Server> serverList = BuildServerList();
            foreach (var server in serverList)
            {
                _ctx.Servers.Add(server);
            }
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
            new Server
            {
                
                Name="Dev-Web",
                IsOnline=true
            },
            new Server
            {
                
                Name="Dev-Mail",
                IsOnline=false
            },
            new Server
            {
                
                Name="CTE-Web",
                IsOnline=true
            },
            new Server
            {
                
                Name="CTE-Mail",
                IsOnline=false
            },new Server
            {
                
                Name="Prod-Web",
                IsOnline=true
            },new Server
            {
                
                Name="Prod-Mail",
                IsOnline=true
            },
            new Server
            {
                
                Name="ITE-Web",
                IsOnline=true
            },
            new Server
            {
                
                Name="ITE-Mail",
                IsOnline=false
            }
            };
        }
    }
}
