using BuildingBlocks.Extensions;
using MediatR;
using System.Reflection;
using TenantService.Core.Entities;
using TenantService.Infrastructure.Data;
using TenantService.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("TenantService.Application"));
builder.Services.ConfigureMySql<TenantContext>(builder.Configuration)
    .AddTransient<ITenantRepository<Tenant>, TenantRepository<Tenant>>();


var app = builder.Build();
app.UsePathBase(new PathString("/tenant-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();