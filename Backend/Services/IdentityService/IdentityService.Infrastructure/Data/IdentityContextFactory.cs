using BuildingBlocks.Multitenancy.DependencyResolver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Data
{
    public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var resolver = new DependencyResolver();

            var config = resolver.GetConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

            return new IdentityContext(optionsBuilder.Options, config, resolver.GetTenantService());
        }
    }
}
