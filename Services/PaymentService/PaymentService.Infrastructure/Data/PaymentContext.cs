using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> opts) : base(opts)
        {

        }

        public DbSet<Payment> Payments { get; set; } = default!;
    }
}
