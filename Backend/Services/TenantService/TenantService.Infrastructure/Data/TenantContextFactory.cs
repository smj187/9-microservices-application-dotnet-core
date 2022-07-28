using BuildingBlocks.Multitenancy.DependencyResolver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Infrastructure.Data
{
    public class TenantContextFactory : IDesignTimeDbContextFactory<TenantContext>
    {
        public TenantContext CreateDbContext(string[] args)
        {
            var resolver = new DependencyResolver();

            var config = resolver.GetConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<TenantContext>();
            var str = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));

            return new TenantContext(optionsBuilder.Options, config, resolver.GetTenantService());
        }
    }
}
