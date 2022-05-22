using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TenantService.API.Extensions;
using TenantService.Application.QueryHandlers;
using TenantService.Application.Repositories;
using TenantService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigureTenant(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<ITenantRepository, TenantRepository>();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();
await app.UseInitialMigration();
app.UseDevEnvironment();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
