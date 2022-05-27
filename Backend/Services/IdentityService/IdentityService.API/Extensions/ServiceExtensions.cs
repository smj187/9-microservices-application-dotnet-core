using IdentityService.Core.Entities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // password settings
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;

                // lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // user settings
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    var issuer = configuration.GetValue<string>("JsonWebToken:Issuer");
                    var audience = configuration.GetValue<string>("JsonWebToken:Audience");
                    var key = configuration.GetValue<string>("JsonWebToken:Key");

                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };

                    var JwkUrl = configuration.GetValue<string>("JwkUrl");

                    // o.RequireHttpsMetadata = true;
                    // //x.SaveToken = true; // keep the public key at Cache for 10 min.
                    // //x.RefreshInterval = TimeSpan.FromSeconds(10);
                    // o.IncludeErrorDetails = true; // <- great for debugging
                    // o.SetJwksOptions(new JwkOptions("https://localhost:5000/jwks"));
                });


            return services;
        }


        public static void UseInitialDatabaseSeeding(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // seed defaults
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    if (!roleManager.Roles.Any())
                    {
                        await roleManager.CreateAsync(new IdentityRole("Administrator"));
                        await roleManager.CreateAsync(new IdentityRole("User"));
                    }

                    if (!userManager.Users.Any())
                    {
                        var admin = new ApplicationUser
                        {
                            UserName = "root",
                            Email = "root@mail.com",
                        };

                        await userManager.CreateAsync(admin, "passwd");
                        await userManager.AddToRoleAsync(admin, "Administrator");
                        await userManager.AddToRoleAsync(admin, "User");


                        var user = new ApplicationUser
                        {
                            UserName = "user",
                            Email = "user@mail.com",
                        };

                        await userManager.CreateAsync(user, "passwd");
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }).Wait();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "error seeding database");
            }
        }


    }
}
