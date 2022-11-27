using BuildingBlocks.Multitenancy.Configurations;
using BuildingBlocks.Multitenancy.Interfaces;
using BuildingBlocks.Multitenancy.Services;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
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
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var audience = configuration.GetValue<string>("JsonWebToken:Audience");
                var expires =configuration.GetValue<string>("JsonWebToken:DurationInMinutes");
                var issuer = configuration.GetValue<string>("JsonWebToken:Issuer");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory()));
            services.AddAuthorization();
            services.AddJwksManager().UseJwtValidation();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<InternalIdentityUser, InternalRole>()
                .AddRoles<InternalRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

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
                serviceCollection.AddJwtAuthentication(config);
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
                        var admin = new InternalIdentityUser(Guid.Parse("00000000-0000-0000-0000-000000000001"), "admin@mail.com", "admin");
                        var appAdmin = new ApplicationUser(tenant.TenantId, Guid.Parse("00000000-0000-0000-0000-000000000001"), admin);

                        await userManager.CreateAsync(admin, "passwd");
                        await userManager.AddToRoleAsync(admin, Role.Administrator.ToString());
                        await userManager.AddToRoleAsync(admin, Role.Moderator.ToString());
                        await userManager.AddToRoleAsync(admin, Role.User.ToString());
                        context.Add(appAdmin);
                        context.Entry(admin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await context.SaveChangesAsync();

                        var mod = new InternalIdentityUser(Guid.Parse("00000000-0000-0000-0000-000000000002"), "mod@mail.com", "mod");
                        var appMod = new ApplicationUser(tenant.TenantId, Guid.Parse("00000000-0000-0000-0000-000000000002"), mod);

                        await userManager.CreateAsync(mod, "passwd");
                        await userManager.AddToRoleAsync(mod, Role.Moderator.ToString());
                        await userManager.AddToRoleAsync(mod, Role.User.ToString());
                        context.Add(appMod);
                        context.Entry(mod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await context.SaveChangesAsync();

                        var user = new InternalIdentityUser(Guid.Parse("00000000-0000-0000-0000-000000000003"), "user@mail.com", "user");
                        var appUser = new ApplicationUser(tenant.TenantId, Guid.Parse("00000000-0000-0000-0000-000000000003"), user);

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
