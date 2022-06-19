using BuildingBlocks.EfCore.Extensions;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using MassTransit;
using MediatR;
using PaymentService.Application.Consumers;
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

builder.Services.AddPostgresDatabase<PaymentContext>(builder.Configuration)
    .AddTransient<IPaymentRepository<Payment>, PaymentRepository<Payment>>();


builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("PaymentService.Application"));

    x.UsingRabbitMq((context, rabbit) =>
    {
        rabbit.Host(RabbitMqSettings.Host, RabbitMqSettings.VirtualHost, host =>
        {
            host.Username(RabbitMqSettings.Username);
            host.Password(RabbitMqSettings.Password);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaPaymentConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<PaymentConsumer>(context);
        });
    });
});


var app = builder.Build();
app.UsePathBase(new PathString("/payment-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();