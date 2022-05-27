
using BuildingBlocks.Extensions;
using IdentityService.API;
using IdentityService.API.Extensions;
using IdentityService.API.Middleware;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using IdentityService.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetDevPack.Security.Jwt.Core.Jwa;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(Assembly.Load("IdentityService.Application"));

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApis())
    .AddInMemoryApiScopes(Config.GetApiScope())
    .AddInMemoryClients(Config.GetClients());


builder.Services.AddJwksManager(opts =>
{
    opts.Jws = Algorithm.Create(AlgorithmType.RSA, JwtType.Jws);
    opts.Jwe = Algorithm.Create(AlgorithmType.RSA, JwtType.Jwe);
})
    .IdentityServer4AutoJwksManager();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.ConfigureMySql<IdentityContext>(builder.Configuration)
    .ConfigureIdentity(builder.Configuration)
    .AddTransient<IUserService, UserService>()
    .AddTransient<IAuthService, AuthService>();


var app = builder.Build();
app.UsePathBase(new PathString("/identity-service"));
app.UseRouting();
app.UseJwksDiscovery();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.UseMiddleware<AuthenticationMiddleware>();
app.UseInitialDatabaseSeeding();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapControllers();
app.Run();
