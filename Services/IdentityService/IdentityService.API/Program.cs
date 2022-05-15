
using IdentityService.API.Extensions;
using IdentityService.Application.Adapters;
using IdentityService.Application.Services;
using IdentityService.Core.Models;
using IdentityService.Core.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IdentityService.Application.Services.IIdentityService, IdentityService.Application.Services.IdentityService>();
builder.Services.AddTransient<IAuthAdapter, AuthAdapter>();


builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.UseDevEnvironment();
app.UseInitialDatabaseSeeding();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
