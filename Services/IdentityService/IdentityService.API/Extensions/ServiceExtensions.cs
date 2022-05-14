using IdentityService.Core.Models;
using IdentityService.Core.Settings;
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
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();

            services.Configure<JsonWebTokenSettings>(configuration.GetSection("JsonWebToken"));

            services.AddDbContext<IdentityContext>(opts =>
            {
                var str = configuration.GetConnectionString("DefaultConnection");
                opts.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));
            });


            
            // configure identity
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

            services.ConfigureApplicationCookie(options =>
            {
                // cookie settings
                options.Cookie.HttpOnly = false;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });



            // configure jwt
            var jwt = configuration.GetSection("JsonWebToken");
            var key = jwt.GetSection("Key").Value;
            var issuer = jwt.GetSection("Issuer").Value;
            var audience = jwt.GetSection("Audience").Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
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
                });
        }

        public static void UseInitialDatabaseSeeding(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // seed defaults
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


                if(!roleManager.Roles.Any())
                {
                    Task.Run(async () =>
                    {
                        await roleManager.CreateAsync(new IdentityRole("Administrator"));
                        await roleManager.CreateAsync(new IdentityRole("User"));
                    }).Wait();
                }

                

                // seed root user
                if (!userManager.Users.Any())
                {
                    Task.Run(async () =>
                    {
                        var defaultUser = new User
                        {
                            UserName = "root",
                            Email = "root@mail.com",
                            CreatedAt = DateTimeOffset.Now
                        };

                        await userManager.CreateAsync(defaultUser, "Pa$$w0rd.");
                        await userManager.AddToRoleAsync(defaultUser, "Administrator");
                        await userManager.AddToRoleAsync(defaultUser, "User");
                    }).Wait();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "error seeding database");
            }
        }

        public static void UseDevEnvironment(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
