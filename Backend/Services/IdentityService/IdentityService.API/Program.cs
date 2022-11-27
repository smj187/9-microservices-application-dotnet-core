using BuildingBlocks.Authentication.Middleware;
using BuildingBlocks.EfCore.Interfaces;
using BuildingBlocks.EfCore.Repositories;
using BuildingBlocks.Middleware.Authentication;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Multitenancy.Extensions;
using BuildingBlocks.Multitenancy.Interfaces;
using BuildingBlocks.Multitenancy.Services;
using IdentityService.API.Extensions;
using IdentityService.Application.Services;
using IdentityService.Application.Services.Interfaces;
using IdentityService.Core.Aggregates;
using IdentityService.Infrastructure.Data;
using IdentityService.Infrastructure.Repositories;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IMultitenancyService, MultitenancyService>();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("IdentityService.Application"));
builder.Services.AddCors();
builder.Services.AddLogging();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddTransient<JwtValidationMiddleware>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddTransient<RoleBaseAuthenticationMiddleware>();

builder.Services.AddMySqlMultitenancy<IdentityContext>(builder.Configuration)
    .AddScoped<IUnitOfWork, UnitOfWork<IdentityContext>>()
    .AddIdentity()
    .AddTransient<IUserService, UserService>()
    .AddTransient<ITokenService, TokenService>()
    .AddTransient<IAdminService, AdminService>()
    .AddTransient<IApplicationUserRepository, ApplicationUserRepository>();



var app = builder.Build();
app.UsePathBase(new PathString("/identity-service"));
app.UsePathBase(new PathString("/id"));
app.UseRouting();
app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithOrigins("https://localhost:4000")
                  .WithOrigins("https://localhost:4001"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseMiddleware<JwtValidationMiddleware>();
app.UseMiddleware<RoleBaseAuthenticationMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseJwksDiscovery();
app.UseIdentitySeeding(builder.Configuration);
app.MapControllers();
app.Run();
