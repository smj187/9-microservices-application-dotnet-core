using BasketService.Core.Domain;
using BasketService.Infrastructure.Repositories;
using BuildingBlocks.Middleware;
using MediatR;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("BasketService.Application"));


var str = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IConnectionMultiplexer>(opt => ConnectionMultiplexer.Connect(str));
builder.Services.AddSingleton<IBasketRepository, BasketRepository>();







var app = builder.Build();
app.UsePathBase(new PathString("/basket-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
