using BuildingBlocks.Extensions;
using BuildingBlocks.MassTransit;
using BuildingBlocks.Middleware;
using CloudinaryDotNet;
using FileService.Application.Services;
using FileService.Core.Domain.Image;
using FileService.Core.Domain.User;
using FileService.Core.Domain.Video;
using FileService.Infrastructure.Data;
using FileService.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("FileService.Application"));

builder.Services.ConfigureMySql<FileContext>(builder.Configuration)
    .AddTransient<IAvatarRepository, AvatarRepository>()
    .AddTransient<IImageRepository, ImageRepository>()
    .AddTransient<IVideoRepository, VideoRepository>()
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
    x.AddConsumers(Assembly.Load("FileService.Application"));
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(RabbitMqSettings.RabbitMqUri);
        config.ConfigureEndpoints(context, new SnakeCaseEndpointNameFormatter("file", false));
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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
