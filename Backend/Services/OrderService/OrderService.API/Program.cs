using BuildingBlocks.EfCore.Extensions;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using MassTransit;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using OrderService.Application.Consumers;
using OrderService.Application.StateMachines;
using OrderService.Application.StateMachines.Events;
using OrderService.Application.StateMachines.Responses;
using OrderService.Contracts.v1;
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

builder.Services.AddPostgresDatabase<OrderContext>(builder.Configuration);

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("OrderService.Application"));

    x.AddSagaStateMachine<OrderStateMachine, OrderStateMachineInstance>()
    .InMemoryRepository();
        //.MongoDbRepository(r =>
        //{
        //    r.Connection = "mongodb://127.0.0.1";
        //    r.DatabaseName = "orders";

        //    r.CollectionName = "sagas";
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

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaBasketCheckoutConsumerEndpointName, endpoint =>
        {
            endpoint.ConfigureConsumer<CreateNewOrderConsumer>(context);
        });
    });
});




var app = builder.Build();
app.UsePathBase(new PathString("/order-service"));
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