using BuildingBlocks.Multitenancy.DependencyResolver;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace IdentityService.Infrastructure.Data
{
    public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var resolver = new DependencyResolver();

            var config = resolver.GetConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            var str = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));

            return new IdentityContext(optionsBuilder.Options, config, resolver.GetTenantService());
        }
    }
}
