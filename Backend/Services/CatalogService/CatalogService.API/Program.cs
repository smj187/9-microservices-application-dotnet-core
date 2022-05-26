using BuildingBlocks.Extensions;
using CatalogService.Core.Entities;
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
    .AddMongoRepository<Category>("categories")
    .AddMongoRepository<Group>("groups")
    .AddMongoRepository<Product>("products");




var app = builder.Build();
app.UsePathBase(new PathString("/ca"));
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
