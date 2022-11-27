using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildingBlocks.Middleware.Exceptions
{
    public class GlobalMultitenancyExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalMultitenancyExceptionMiddleware> _logger;
        private readonly IEnumerable<TenantConfiguration> _tenants;

        public GlobalMultitenancyExceptionMiddleware(ILogger<GlobalMultitenancyExceptionMiddleware> logger, IConfiguration config)
        {
            _logger = logger;
            var tenants = config.GetSection("tenants").Get<IEnumerable<TenantConfiguration>>();
            if (tenants == null)
            {
                throw new ConfigurationException("failed to load tenants from configuration");
            }
            _tenants = tenants;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.Headers.TryGetValue("tenant-id", out var tenantId);
            
            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrWhiteSpace(tenantId))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var json = JsonSerializer.Serialize(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = $"Incorrect Header",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = $"Missing tenant id in header",
                    Instance = context.Request.Path,
                });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            else if (_tenants.FirstOrDefault(t => t.TenantId == tenantId) == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var json = JsonSerializer.Serialize(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = $"Incorrect Tenant Header",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = $"No tenant with {tenantId} exists",
                    Instance = context.Request.Path,
                });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            else
            {
                await next(context);
            }
        }
    }
}
