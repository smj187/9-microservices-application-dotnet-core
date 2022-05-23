using CatalogService.API.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureMongo(builder.Configuration)
    .AddProductRepository("products")
    .AddCategoryRepository("categories");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();
app.UseDevEnvironment();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
