using BuildingBlocks.Cache.Extensions;
using BuildingBlocks.EfCore.Repositories;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Multitenancy.Extensions;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using CloudinaryDotNet;
using FileService.Application.Services;
using FileService.Core.Domain.Aggregates;
using FileService.Infrastructure.Data;
using FileService.Infrastructure.Repositories;
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
builder.Services.AddMediatR(Assembly.Load("FileService.Application"));

builder.Services.AddMySqlMultitenancy<FileContext>(builder.Configuration)
    .AddScoped<IUnitOfWork, UnitOfWork<FileContext>>()
    .AddTransient<IAssetRepository, AssetRepository>()
    .AddTransient<ICloudService, CloudService>();

// make sure these values are set using dotnet user-secrets
var cloudName = builder.Configuration.GetValue<string>("Cloudinary:CloudName");
var apiKey = builder.Configuration.GetValue<string>("Cloudinary:ApiKey");
var apiSecret = builder.Configuration.GetValue<string>("Cloudinary:ApiSecret");

if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrWhiteSpace))
{
    throw new ArgumentException("Please specify Cloudinary account details!");
}

builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));


builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("FileService.Application"));

    x.UsingRabbitMq((context, rabbit) =>
    {
        var host = builder.Configuration.GetValue<string>("RabbitMq:Host");
        var username = builder.Configuration.GetValue<string>("RabbitMq:Username");
        var password = builder.Configuration.GetValue<string>("RabbitMq:Password");

        rabbit.Host(new Uri(host), host =>
        {
            host.Username(username);
            host.Password(password);
        });
    });
});


var app = builder.Build();
app.UsePathBase(new PathString("/file-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<MultitenancyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
