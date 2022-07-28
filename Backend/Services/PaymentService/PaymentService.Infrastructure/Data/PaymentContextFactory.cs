using BuildingBlocks.Multitenancy.DependencyResolver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Data
{
    public class PaymentContextFactory : IDesignTimeDbContextFactory<PaymentContext>
    {
        public PaymentContext CreateDbContext(string[] args)
        {
            var resolver = new DependencyResolver();

            var config = resolver.GetConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            var str = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(str);

            return new PaymentContext(optionsBuilder.Options, config, resolver.GetTenantService());
        }
    }
}
