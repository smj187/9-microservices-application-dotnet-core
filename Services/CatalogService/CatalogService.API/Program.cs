using MediatR;
using CatalogService.Application.Queries;
using CatalogService.Infrastructure.Data;
using CatalogService.API.Extensions;


// builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureMongo(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(ListProductsQuery));
builder.Services.AddMediatR(typeof(ListCategoriesQuery));



// app
var app = builder.Build();
app.UseDevEnvironment();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
