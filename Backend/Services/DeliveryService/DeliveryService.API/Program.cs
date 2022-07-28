using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Mongo.Configurations;
using BuildingBlocks.Mongo.Extensions;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using DeliveryService.Application.Consumers;
using DeliveryService.Core.Domain.Aggregates;
using DeliveryService.Infrastructure.BsonClassMapDefinitions;
using DeliveryService.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IMultitenancyService, MultitenancyService>();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("DeliveryService.Application"));

builder.Services.AddMongoDatabase(builder.Configuration)
    .AddTransient<IDeliveryRepository, DeliveryRepository>()
    .AddEntityBaseMongoConfiguration()
    .AddBsonClassMappings();

builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("DeliveryService.Application"));

    x.UsingRabbitMq((context, rabbit) =>
    {
        rabbit.Host(RabbitMqSettings.Host, RabbitMqSettings.VirtualHost, host =>
        {
            host.Username(RabbitMqSettings.Username);
            host.Password(RabbitMqSettings.Password);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaDeliveryConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<DeliverySagaConsumer>(context);
        });
    });

});


var app = builder.Build();
app.UsePathBase(new PathString("/delivery-service"));
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
