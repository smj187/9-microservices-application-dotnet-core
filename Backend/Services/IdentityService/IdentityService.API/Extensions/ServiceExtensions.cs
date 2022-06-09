using IdentityService.Core.Entities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Jwa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryApiScopes(Config.GetApiScope())
                .AddInMemoryClients(Config.GetClients());


            services.AddJwksManager(opts =>
            {
                opts.Jws = Algorithm.Create(AlgorithmType.RSA, JwtType.Jws);
                opts.Jwe = Algorithm.Create(AlgorithmType.RSA, JwtType.Jwe);
            })
                .IdentityServer4AutoJwksManager();

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory()));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();


            return services;
        }

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
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // user settings
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
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
                        await roleManager.CreateAsync(new IdentityRole(Role.Administrator.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Role.Moderator.ToString()));
                        await roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
                    }

                    if (!userManager.Users.Any())
                    {
                        var admin = new ApplicationUser
                        {
                            UserName = "admin",
                            Email = "admin@mail.com",
                        };

                        await userManager.CreateAsync(admin, "passwd");
                        await userManager.AddToRoleAsync(admin, Role.Administrator.ToString());
                        await userManager.AddToRoleAsync(admin, Role.Moderator.ToString());
                        await userManager.AddToRoleAsync(admin, Role.User.ToString());

                        var mod = new ApplicationUser
                        {
                            UserName = "mod",
                            Email = "mod@mail.com",
                        };

                        await userManager.CreateAsync(mod, "passwd");
                        await userManager.AddToRoleAsync(mod, Role.Moderator.ToString());
                        await userManager.AddToRoleAsync(mod, Role.User.ToString());

                        var user = new ApplicationUser
                        {
                            UserName = "user",
                            Email = "user@mail.com",
                        };

                        await userManager.CreateAsync(user, "passwd");
                        await userManager.AddToRoleAsync(user, Role.User.ToString());
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
