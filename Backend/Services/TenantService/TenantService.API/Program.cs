using BuildingBlocks.EfCore.Repositories;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Multitenancy.Extensions;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using MassTransit;
using MediatR;
using System.Reflection;
using TenantService.Application.Consumers;
using TenantService.Core.Domain.Aggregates;
using TenantService.Infrastructure.Data;
using TenantService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IMultitenancyService, MultitenancyService>();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("TenantService.Application"));



builder.Services.AddMySqlMultitenancy<TenantContext>(builder.Configuration)
    .AddScoped<IUnitOfWork, UnitOfWork<TenantContext>>()
    .AddTransient<ITenantRepository, TenantRepository>()
    .AddTransient<IOrderRepository, OrderRepository>();

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
            e.ConfigureConsumer<TenantSagaConsumer>(context);
        });

        // add banner to tenant
        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadAddBannerToTenantConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddBannerToTenantConsumer>(context);
        });

        // add brand image to tenant
        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadAddBrandImageToTenantConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddBrandImageToTenantConsumer>(context);
        });

        // add logo to tenant
        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadAddLogoToTenantConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddLogoToTenantConsumer>(context);
        });

        // add video to tenant
        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadAddVideoToTenantConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddVideoToTenantConsumer>(context);
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
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<MultitenancyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();