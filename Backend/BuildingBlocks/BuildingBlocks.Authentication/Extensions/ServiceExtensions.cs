using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Authentication.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJwtValidation(this IServiceCollection services, IConfiguration configuration)
        {
            

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    var audience = configuration.GetValue<string>("JsonWebToken:Audience");
            //    var expires = configuration.GetValue<string>("JsonWebToken:DurationInMinutes");
            //    var issuer = configuration.GetValue<string>("JsonWebToken:Issuer");

            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = issuer,
            //        ValidAudience = audience
            //    };
            //});

            services.AddAuthorization();
            services.AddJwksManager().UseJwtValidation();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();


            return services;
        }
    }
}
