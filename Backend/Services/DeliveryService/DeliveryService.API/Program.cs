using BuildingBlocks.Extensions;
using DeliveryService.Application.QueryHandlers;
using DeliveryService.Core.Entities;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongo(builder.Configuration)
    .AddMongoRepository<Delivery>("deliveries");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(ListDeliveriesQueryHandler).Assembly);


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
