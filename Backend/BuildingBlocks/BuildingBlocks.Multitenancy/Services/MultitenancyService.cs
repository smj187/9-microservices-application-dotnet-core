using BuildingBlocks.Multitenancy.Configurations;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Multitenancy.Services
{
    public class MultitenancyService : IMultitenancyService
    {
        private readonly string _tenantId = default!;
        private readonly string _connectionString = default!;

        public MultitenancyService(string tenantId, IConfiguration configuration)
        {
            _tenantId = tenantId;

            var tenants = configuration.GetSection("tenants").Get<IEnumerable<TenantConfiguration>>();
            var tenant = tenants.FirstOrDefault(t => t.TenantId == tenantId);
            if (tenant != null)
            {
                _connectionString = tenant.ConnectionString;
            }
            else
            {
                throw new Exception($"no such tenant {tenantId}");
            }
        }

        public MultitenancyService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                httpContextAccessor.HttpContext.Request.Headers.TryGetValue("tenant-id", out var tenantId);
                Console.WriteLine($"found -> {tenantId}");

                var tenants = configuration.GetSection("tenants").Get<IEnumerable<TenantConfiguration>>();
                var tenant = tenants.FirstOrDefault(t => t.TenantId == tenantId);
                if (tenant != null)
                {
                    _tenantId = tenant.TenantId;
                    _connectionString = tenant.ConnectionString;
                }
                else
                {
                    throw new Exception($"no such tenant {tenantId}");
                }
            }
            else
            {
                Console.WriteLine("no httpContextAccessor");
            }
        }

        public string GetTenantId() => _tenantId;
        public string GetConnectionString() => _connectionString;
    }
}
