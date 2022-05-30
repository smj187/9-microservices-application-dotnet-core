using BuildingBlocks.Extensions;
using BuildingBlocks.MassTransit;
using CatalogService.Contracts.v1.Events;
using CatalogService.Core.Entities;
using MassTransit;
using MediatR;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("CatalogService.Application"));

builder.Services.ConfigureMongo(builder.Configuration)
    .AddMongoRepository<Category>("categories")
    .AddMongoRepository<Group>("groups")
    .AddMongoRepository<Product>("products");




builder.Services.AddMassTransit(x =>
{
  

    x.UsingRabbitMq((context, config) =>
    {
        // default rabbitmq setup
        config.Host(new Uri("rabbitmq://localhost/"), h =>
        {
            h.Username(RabbitMqSettings.Username);
            h.Password(RabbitMqSettings.Password);
        });


        config.ConfigureEndpoints(context);


        config.UseMessageRetry(retryConfig => retryConfig.Interval(5, TimeSpan.FromMilliseconds(250)));

    });

    // RESPONSE
    x.AddRequestClient<RequestResponseEvent>(new Uri("queue:response-queue"), RequestTimeout.After(s:10));
});


var app = builder.Build();
app.UsePathBase(new PathString("/catalog-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
