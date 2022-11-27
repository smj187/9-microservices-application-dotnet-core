using BuildingBlocks.Authentication.Extensions;
using BuildingBlocks.Authentication.Middleware;
using BuildingBlocks.Cache.Extensions;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Mongo.Configuration;
using BuildingBlocks.Mongo.Extensions;
using BuildingBlocks.Multitenancy.Interfaces;
using BuildingBlocks.Multitenancy.Services;
using CatalogService.Core.Domain.Categories;
using CatalogService.Core.Domain.Products;
using CatalogService.Core.Domain.Sets;
using CatalogService.Infrastructure.BsonClassMapDefinitions;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NetDevPack.Security.JwtExtensions;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IMultitenancyService, MultitenancyService>();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("CatalogService.Application"));
builder.Services.AddCors();
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<RoleBaseAuthenticationMiddleware>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddTransient<GlobalMultitenancyExceptionMiddleware>();

builder.Services.AddMongoDatabase(builder.Configuration)
    .AddTransient<IProductRepository, ProductRepository>()
    .AddTransient<ICategoryRepository, CategoryRepository>()
    .AddTransient<ISetRepository, SetRepository>()
    .AddEntityBaseMongoConfiguration()
    .AddBsonClassMappings();


builder.Services.AddCaching(builder.Configuration);



var app = builder.Build();
app.UsePathBase(new PathString("/catalog-service"));
app.UsePathBase(new PathString("/ca"));
app.UseRouting();
app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithOrigins("https://localhost:4000")
                  .WithOrigins("https://localhost:4001"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseMiddleware<RoleBaseAuthenticationMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<GlobalMultitenancyExceptionMiddleware>();
app.MapControllers();
app.Run();