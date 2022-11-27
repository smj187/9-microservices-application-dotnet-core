using BuildingBlocks.Attributes;
using BuildingBlocks.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Authentication.Middleware
{
    public class RoleBaseAuthenticationMiddleware : IMiddleware
    {
        private readonly ILogger<RoleBaseAuthenticationMiddleware> _logger;

        public RoleBaseAuthenticationMiddleware(ILogger<RoleBaseAuthenticationMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            if (endpoint == null)
            {
                throw new Exception("failed to find endpoint");
            }
            var authenticationAttribute = endpoint.Metadata.GetMetadata<GatewayAuthenticationAttribute>();

            // if there is no attribute, just continue
            if (authenticationAttribute == null || authenticationAttribute.RequiredUserRole == null)
            {
                await next(context);
            }
            else
            {
                // get roles injected by identity service
                context.Request.Headers.TryGetValue("x-auth-user-roles", out var xAuthUserRoles);
                if (string.IsNullOrEmpty(xAuthUserRoles))
                {
                    await context.ResponseWithProblem((int)HttpStatusCode.Forbidden, "Forbidden Request", "Incomplete header, missing user roles for x-auth-user-roles values.");
                }
                else
                {
                    // check if user roles containe authentication attribute user role
                    var role = authenticationAttribute.RequiredUserRole.ToLower();
                    var userRoles = xAuthUserRoles.ToString().Split(';');

                    if (userRoles.Contains(role))
                    {
                        await next(context);
                    }
                    else
                    {
                        await context.ResponseWithProblem((int)HttpStatusCode.Forbidden, "Forbidden Request", $"Incomplete header, user role {role} was required but was not found.");
                    }
                }
            }
        }
    }
}
