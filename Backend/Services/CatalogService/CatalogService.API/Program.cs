using BuildingBlocks.Extensions;
using BuildingBlocks.MassTransit;
using BuildingBlocks.Middleware;
using CatalogService.Application.Consumers;
using CatalogService.Core.Domain.Categories;
using CatalogService.Core.Domain.Products;
using CatalogService.Core.Domain.Sets;
using CatalogService.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("CatalogService.Application"));

builder.Services.ConfigureMongo(builder.Configuration)
    .AddSingleton<IProductRepository, ProductRepository>()
    .AddSingleton<ICategoryRepository, CategoryRepository>()
    .AddSingleton<ISetRepository, SetRepository>();



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
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
