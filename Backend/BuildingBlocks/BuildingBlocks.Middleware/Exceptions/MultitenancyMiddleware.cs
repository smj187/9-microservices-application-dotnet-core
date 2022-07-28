using BuildingBlocks.Middleware.Models;
using BuildingBlocks.Multitenancy.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Middleware.Exceptions
{
    public class MultitenancyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IReadOnlyCollection<TenantConfiguration> _tenants;

        public MultitenancyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _tenants = config.GetSection("tenants").Get<IEnumerable<TenantConfiguration>>().ToList();
        }

        private static async Task HandleTenantNotFoundResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsync(new ProblemResponse
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                title = "Bad Request",
                status = context.Response.StatusCode,
                detail = "invalid or missing tenant id",
                instance = context.Request.Path,
                traceId = "TODO: implement"
            }.ToString());
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers.TryGetValue("tenant-id", out var tenantId);

            if (string.IsNullOrEmpty(tenantId) || string.IsNullOrWhiteSpace(tenantId))
            {
                await HandleTenantNotFoundResponse(context);
            }
            else if (_tenants.FirstOrDefault(t => t.TenantId == tenantId) == null)
            {
                await HandleTenantNotFoundResponse(context);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
