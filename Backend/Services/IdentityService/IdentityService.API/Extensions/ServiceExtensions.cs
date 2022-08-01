using BuildingBlocks.Multitenancy.Configurations;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            services.AddIdentity<InternalIdentityUser, InternalRole>()
                .AddRoles<InternalRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

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

        public static void UseIdentitySeeding(this WebApplication app, IConfiguration config)
        {
            var tenants = config.GetSection("tenants").Get<IEnumerable<TenantConfiguration>>();

            foreach (var tenant in tenants)
            {
                var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

                var serviceCollection = new ServiceCollection();
                serviceCollection.AddTransient<IEnvironmentService, EnvironmentService>();
                serviceCollection.AddTransient<IConfigurationService, ConfigurationService>(serviceProvider =>
                {
                    return new ConfigurationService(serviceProvider.GetRequiredService<IEnvironmentService>(), Directory.GetCurrentDirectory());
                });

                serviceCollection.AddTransient(serviceProvider =>
                {
                var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
                    var str = tenant.ConnectionString;
                    optionsBuilder.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));
                    var tenantService = new MultitenancyService(tenant.TenantId, config);
                    return new IdentityContext(optionsBuilder.Options, config, tenantService);
                });

                serviceCollection.ConfigureIdentity(config);
                serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                serviceCollection.AddTransient<IMultitenancyService>(serviceProvider =>
                {
                    var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                    return new MultitenancyService(httpContext, config);
                });

                serviceCollection.AddIdentity<InternalIdentityUser, InternalRole>()
                    .AddRoles<InternalRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

                serviceCollection.AddLogging();
                var services = serviceCollection.BuildServiceProvider();


                using var scope = app.Services.CreateScope();
                var userManager = services.GetRequiredService<UserManager<InternalIdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<InternalRole>>();
                var context = services.GetRequiredService<IdentityContext>();

                Task.Run(async () =>
                {
                    if (!roleManager.Roles.Any())
                    {
                        await roleManager.CreateAsync(new InternalRole(Role.Administrator.ToString()));
                        await roleManager.CreateAsync(new InternalRole(Role.Moderator.ToString()));
                        await roleManager.CreateAsync(new InternalRole(Role.User.ToString()));
                    }

                    if (!userManager.Users.Any())
                    {
                        var admin = new InternalIdentityUser(Guid.Parse("11111111-2222-3333-4444-000000000000"), "admin@mail.com", "admin");
                        var appAdmin = new ApplicationUser(tenant.TenantId, Guid.Parse("11111111-2222-3333-4444-000000000000"), admin);

                        await userManager.CreateAsync(admin, "passwd");
                        await userManager.AddToRoleAsync(admin, Role.Administrator.ToString());
                        await userManager.AddToRoleAsync(admin, Role.Moderator.ToString());
                        await userManager.AddToRoleAsync(admin, Role.User.ToString());
                        context.Add(appAdmin);
                        context.Entry(admin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await context.SaveChangesAsync();


                        var mod = new InternalIdentityUser(Guid.Parse("11111111-2222-3333-4444-000000000001"), "mod@mail.com", "mod");
                        var appMod = new ApplicationUser(tenant.TenantId, Guid.Parse("11111111-2222-3333-86bd-000000000001"), mod);

                        await userManager.CreateAsync(mod, "passwd");
                        await userManager.AddToRoleAsync(mod, Role.Moderator.ToString());
                        await userManager.AddToRoleAsync(mod, Role.User.ToString());
                        context.Add(appMod);
                        context.Entry(mod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await context.SaveChangesAsync();

                        var user = new InternalIdentityUser(Guid.Parse("11111111-2222-3333-4444-000000000002"), "user@mail.com", "user");
                        var appUser = new ApplicationUser(tenant.TenantId, Guid.Parse("11111111-2222-3333-86bd-000000000002"), user);

                        await userManager.CreateAsync(user, "passwd");
                        await userManager.AddToRoleAsync(user, Role.User.ToString());
                        context.Add(appUser);
                        context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await context.SaveChangesAsync();
                    }
                }).Wait();
            }
        }
    }
}
