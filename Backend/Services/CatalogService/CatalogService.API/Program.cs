using BuildingBlocks.Cache.Extensions;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Mongo.Configurations;
using BuildingBlocks.Mongo.Extensions;
using CatalogService.Application.Consumers;
using CatalogService.Core.Domain.Categories;
using CatalogService.Core.Domain.Products;
using CatalogService.Core.Domain.Sets;
using CatalogService.Infrastructure.BsonClassMapDefinitions;
using CatalogService.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("CatalogService.Application"));

builder.Services.AddMongoDatabase(builder.Configuration)
    .AddSingleton<IProductRepository, ProductRepository>()
    .AddSingleton<ICategoryRepository, CategoryRepository>()
    .AddSingleton<ISetRepository, SetRepository>()
    .AddEntityBaseMongoConfiguration()
    .AddBsonClassMappings();

builder.Services.AddCaching(builder.Configuration);


builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("CatalogService.Application"));

    x.UsingRabbitMq((context, rabbit) =>
    {
        rabbit.Host(RabbitMqSettings.Host, RabbitMqSettings.VirtualHost, host =>
        {
            host.Username(RabbitMqSettings.Username);
            host.Password(RabbitMqSettings.Password);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaCatalogConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<CatalogConsumer>(context);
        });

        // category
        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadCategoryImageConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddImageToCategoryConsumer>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadCategoryVideoConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddVideoToCategoryConsumer>(context);
        });

        // product
        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadProductImageConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddImageToProductConsumer>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadProductVideoConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddVideoToProductConsumer>(context);
        });

        // set
        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadSetImageConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddImageToSetConsumer>(context);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadSetVideoConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddVideoToSetConsumer>(context);
        });
    });
});



var app = builder.Build();
app.UsePathBase(new PathString("/catalog-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
