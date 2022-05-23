using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.API.Extensions;
using OrderService.Application.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();
await app.UseInitialMigration();
app.UseDevEnvironment();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
