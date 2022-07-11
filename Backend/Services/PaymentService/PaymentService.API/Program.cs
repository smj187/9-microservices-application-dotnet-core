using BuildingBlocks.EfCore.Extensions;
using BuildingBlocks.EfCore.Repositories;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Multitenancy.Extensions;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using MassTransit;
using MediatR;
using PaymentService.Application.Consumers;
using PaymentService.Core.Domain.Aggregates;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IMultitenancyService, MultitenancyService>();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("PaymentService.Application"));

//builder.Services.AddPostgresDatabase<PaymentContext>(builder.Configuration)
//    .AddTransient<IPaymentRepository<Payment>, PaymentRepository<Payment>>();

builder.Services.AddPostgresMultitenancy<PaymentContext>(builder.Configuration)
    .AddScoped<IUnitOfWork, UnitOfWork<PaymentContext>>()
    .AddTransient<IPaymentRepository, PaymentRepository>();

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
            e.ConfigureConsumer<PaymentSagaConsumer>(context);
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
app.UseMiddleware<MultitenancyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();