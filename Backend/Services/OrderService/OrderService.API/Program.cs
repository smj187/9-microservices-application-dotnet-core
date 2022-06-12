using BuildingBlocks.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Core.Entities;
using OrderService.Infrastructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("OrderService.Application"));

builder.Services.ConfigureNpgsql<OrderContext>(builder.Configuration);


var app = builder.Build();
app.UsePathBase(new PathString("/order-service"));
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