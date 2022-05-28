using BuildingBlocks.Extensions;
using CloudinaryDotNet;
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