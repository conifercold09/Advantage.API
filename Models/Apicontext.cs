using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advantage.API.Models
{
    public class Apicontext : DbContext
    {
        public Apicontext(DbContextOptions<Apicontext> options) : base(options) { }

        public DbSet<Customer> Customers {get;set;}
        public DbSet<Order> Orders {get;set;}
        public DbSet<Server> Servers {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
           // options.UseSqlServer("Data Source=DESKTOP-4BF817M\\SQLEXPRESS;database=master;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
