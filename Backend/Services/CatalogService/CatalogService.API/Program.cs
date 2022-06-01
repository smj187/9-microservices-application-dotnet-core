using BuildingBlocks.Extensions;
using BuildingBlocks.Middleware;
using CatalogService.Core.Domain.Category;
using CatalogService.Core.Domain.Group;
using CatalogService.Core.Domain.Product;
using CatalogService.Infrastructure.Repositories;
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
    .AddSingleton<IGroupRepository, GroupRepository>();




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
