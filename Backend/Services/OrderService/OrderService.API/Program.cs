using BuildingBlocks.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Core.Entities;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("OrderService.Application"));

builder.Services.ConfigureNpgsql<OrderContext>(builder.Configuration)
    .AddTransient<IOrderRepository<Order>, OrderRepository<Order>>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();