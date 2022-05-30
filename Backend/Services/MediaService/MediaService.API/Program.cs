using BuildingBlocks.Extensions;
using BuildingBlocks.MassTransit;
using CloudinaryDotNet;
using MassTransit;
using MediaService.Application.Consumers;
using MediaService.Application.Services;
using MediaService.Core.Entities;
using MediaService.Infrastructure.Data;
using MediaService.Infrastructure.Repositories;
using MediatR;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("MediaService.Application"));
builder.Services.ConfigureMySql<MediaContext>(builder.Configuration)
    .AddTransient<IImageRepository<ImageBlob>, ImageRepository<ImageBlob>>()
    .AddTransient<ICloudService, CloudService>();


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
    //// RESPONSE
    //x.AddConsumer<RequestRequestEventConsumer>();

    //// PUBLISH
    //x.AddConsumer<PubishEventConsumer>();

    x.AddConsumers(Assembly.Load("MediaService.Application"));

    x.UsingRabbitMq((context, config) =>
    {
        // default rabbitmq setup
        config.Host(new Uri("rabbitmq://localhost/"), h =>
        {
            h.Username(RabbitMqSettings.Username);
            h.Password(RabbitMqSettings.Password);
        });

        config.UseMessageRetry(retryConfig => retryConfig.Interval(5, TimeSpan.FromMilliseconds(250)));




        // RESPONSE
        config.ReceiveEndpoint("queue:response-queue", e =>
        {
            e.ConfigureConsumer<RequestRequestEventConsumer>(context);
        });


        // PUBLISH
        config.ReceiveEndpoint("queue:publish-queue", e =>
        {
            e.ConfigureConsumer<PubishEventConsumer>(context);
        });

    });

});


var app = builder.Build();
app.UsePathBase(new PathString("/media-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();