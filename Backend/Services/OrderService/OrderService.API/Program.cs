using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Mongo.Configurations;
using BuildingBlocks.Mongo.Extensions;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using MassTransit;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using OrderService.API;
using OrderService.Application.Consumers;
using OrderService.Application.StateMachines;
using OrderService.Application.StateMachines.Events;
using OrderService.Application.StateMachines.Responses;
using OrderService.Contracts.v1;
using OrderService.Core.Entities;
using OrderService.Core.Entities.Aggregates;
using OrderService.Core.StateMachines;
using OrderService.Infrastructure.BsonClassMapDefinitions;
using OrderService.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IMultitenancyService, MultitenancyService>();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("OrderService.Application"));


builder.Services.AddMongoDatabase(builder.Configuration)
    .AddTransient<IOrderRepository, OrderRepository>()
    .AddEntityBaseMongoConfiguration()
    .AddBsonClassMappings();

builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("OrderService.Application"));

    x.AddSagaStateMachine<OrderStateMachine, OrderStateMachineInstance>()
        .InMemoryRepository();
    //.MongoDbRepository(r =>
    //{
    //    r.Connection = builder.Configuration.GetSection("SagaPersistence:ConnectionString").Value;
    //    r.DatabaseName = builder.Configuration.GetSection("SagaPersistence:DatabaseName").Value;
    //    r.CollectionName = builder.Configuration.GetSection("SagaPersistence:CollectionName").Value;
    //});


    x.UsingRabbitMq((context, rabbit) =>
    {
        rabbit.Host(RabbitMqSettings.Host, RabbitMqSettings.VirtualHost, host =>
        {
            host.Username(RabbitMqSettings.Username);
            host.Password(RabbitMqSettings.Password);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaName, endpoint =>
        {
            endpoint.ConfigureSaga<OrderStateMachineInstance>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaCompletionConsumerEndpointName, endpoint =>
        {
            endpoint.ConfigureConsumer<CompleteOrderSagaConsumer>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaCatalogUnavailableErrorConsumerEndpointName, endpoint =>
        {
            endpoint.ConfigureConsumer<HandleCatalogUnavailableSagaErrorConsumer>(context);
        });
        
        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaCatalogOutOfStockErrorConsumerEndpointName, endpoint =>
        {
            endpoint.ConfigureConsumer<HandleCatalogOutOfStockSagaErrorConsumer>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaPaymentFailureErrorConsumerEndpointName, endpoint =>
        {
            endpoint.ConfigureConsumer<HandlePaymentFailureSagaErrorConsumer>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaTenantRejectionErrorConsumerEndpointName, endpoint =>
        {
            endpoint.ConfigureConsumer<HandleTenantRejectionSagaErrorConsumer>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaDeliveryFailedErrorConsumerEndpointName, endpoint =>
        {
            endpoint.ConfigureConsumer<HandleDeliveryFailureSagaErrorConsumer>(context);
        });
    });
});

 builder.Services.AddHostedService<MongoBackgroundService>();


var app = builder.Build();
app.UsePathBase(new PathString("/order-service"));
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