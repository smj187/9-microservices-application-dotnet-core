using BuildingBlocks.Extensions;
using CatalogService.Application.Queries;
using CatalogService.Application.QueryHandlers;
using CatalogService.Core.Entities;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongo(builder.Configuration)
    .AddMongoRepository<Category>("categories")
    .AddMongoRepository<Group>("groups")
    .AddMongoRepository<Product>("products");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// TODO: fix
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(ListCategoryQueryHandler).GetTypeInfo().Assembly);
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
