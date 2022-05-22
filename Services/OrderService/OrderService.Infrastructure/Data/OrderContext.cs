using Microsoft.EntityFrameworkCore;
using OrderService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> opts) : base(opts)
        {

        }

        public DbSet<Order> Orders { get; set; } = default!;
    }
}
