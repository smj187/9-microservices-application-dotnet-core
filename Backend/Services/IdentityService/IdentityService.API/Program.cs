
using BuildingBlocks.Extensions;
using IdentityService.API.Extensions;
using IdentityService.Application.Adapters;
using IdentityService.Application.Services;
using IdentityService.Infrastructure.Data;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("IdentityService.Application"));

builder.Services.ConfigureNpgsql<IdentityContext>(builder.Configuration)
    .AddIdentity(builder.Configuration)
    .AddTransient<IUserService, UserService>()
    .AddTransient<IdentityService.Application.Services.IIdentityService, IdentityService.Application.Services.IdentityService>()
    .AddTransient<IAuthAdapter, AuthAdapter>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseInitialDatabaseSeeding();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
