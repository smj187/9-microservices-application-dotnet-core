using BuildingBlocks.Extensions;
using BuildingBlocks.MassTransit;
using BuildingBlocks.Middleware;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Category;
using CatalogService.Core.Domain.Group;
using CatalogService.Core.Domain.Product;
using CatalogService.Infrastructure.Repositories;
using FileService.Contracts.v1;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NetDevPack.Security.JwtExtensions;
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
    .AddSingleton<IGroupRepository, GroupRepository>();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.Load("CatalogService.Application"));
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(RabbitMqSettings.RabbitMqUri);
        config.ConfigureEndpoints(context, new SnakeCaseEndpointNameFormatter("catalog", false));
    });
  
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.SetJwksOptions(new JwkOptions("https://localhost:5000/jwks"));
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
