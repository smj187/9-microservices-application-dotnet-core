using BuildingBlocks.Extensions;
using MediatR;
using PaymentService.Core.Entities;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("PaymentService.Application"));

builder.Services.ConfigureNpgsql<PaymentContext>(builder.Configuration)
    .AddTransient<IPaymentRepository<Payment>, PaymentRepository<Payment>>();


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