using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildingBlocks.Middleware.Authentication
{
    public class JwtValidationMiddleware : IMiddleware
    {
        private readonly ILogger<JwtValidationMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public JwtValidationMiddleware(ILogger<JwtValidationMiddleware> logger, IConfiguration configuration, IJwtService jwtService)
        {
            _logger = logger;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authorization = context.Request.Headers["Authorization"];
            var existingRoles = context.Request.Headers["x-auth-user-roles"];
            // continiue with unauthenticated request if there is no bearer token or request is already authenticated
            if (string.IsNullOrEmpty(authorization) || !string.IsNullOrEmpty(existingRoles))
            {
                await next(context);
            }
            else
            {
                // gateway-added header to add claims to
                var token = authorization.FirstOrDefault()?[7..];

                // check if token is valid
                var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();
                var audience = _configuration.GetValue<string>("JsonWebToken:Audience");
                var issuer = _configuration.GetValue<string>("JsonWebToken:Issuer");

                var result = handler.ValidateToken(token,
                        new TokenValidationParameters
                        {
                            ValidIssuer = issuer,
                            ValidAudience = audience,
                            IssuerSigningKey = await _jwtService.GetCurrentSecurityKey(),
                        });

                // get user roles from claims
                var claims = new List<string>();
                if (result.IsValid)
                {
                    claims = result.ClaimsIdentity.Claims
                        .Where(c => c.Type == "roles")
                        .Select(c => c.Value.ToLower())
                        .ToList();
                }

                // add user roles to identity service route directly
                if (string.IsNullOrEmpty(context.Request.Headers["x-authz-header"]))
                {
                    context.Request.Headers.Add("x-auth-user-roles", string.Join(";", claims));
                    await next(context);
                }
                else
                {
                    // add user roles to authorization_response gateway response
                    context.Response.Headers.Add("x-auth-user-roles", string.Join(";", claims));

                    context.Response.StatusCode = (int)StatusCodes.Status200OK;
                    await context.Response.WriteAsync("success");
                }
            }
        }
    }
}