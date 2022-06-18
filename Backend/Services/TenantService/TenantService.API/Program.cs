using BuildingBlocks.Extensions;
using BuildingBlocks.MassTransit;
using MassTransit;
using MediatR;
using System.Reflection;
using TenantService.Application.Consumers;
using TenantService.Core.Domain.Aggregates;
using TenantService.Infrastructure.Data;
using TenantService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("TenantService.Application"));
builder.Services.ConfigureMySql<TenantContext>(builder.Configuration)
    .AddTransient<ITenantRepository, TenantRepository>();



builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("TenantService.Application"));

    x.UsingRabbitMq((context, rabbit) =>
    {
        rabbit.Host(RabbitMqSettings.Host, RabbitMqSettings.VirtualHost, host =>
        {
            host.Username(RabbitMqSettings.Username);
            host.Password(RabbitMqSettings.Password);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.OrderSagaTenantConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<TenantConsumer>(context);
        });
    });

});



var app = builder.Build();
app.UsePathBase(new PathString("/tenant-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();