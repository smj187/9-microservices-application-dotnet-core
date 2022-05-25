using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Entities;
using PaymentService.Infrastructure.EntityTypeConfigurations;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentEntityTypeConfiguration());
            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Payment> Payments { get; set; } = default!;


        
    }
}
