using BuildingBlocks.EfCore.Repositories;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Multitenancy.Extensions;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using IdentityService.API.Extensions;
using IdentityService.API.Middleware;
using IdentityService.Application.Consumers;
using IdentityService.Application.Services;
using IdentityService.Core.Aggregates;
using IdentityService.Infrastructure.Data;
using IdentityService.Infrastructure.Repositories;
using MassTransit;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IMultitenancyService, MultitenancyService>();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("IdentityService.Application"));

builder.Services.ConfigureIdentityServer();

builder.Services.AddMySqlMultitenancy<IdentityContext>(builder.Configuration)
    .AddScoped<IUnitOfWork, UnitOfWork<IdentityContext>>()
    .ConfigureIdentity(builder.Configuration)
    .AddTransient<IUserService, UserService>()
    .AddTransient<IAdminService, AdminService>()
    .AddTransient<ITokenService, TokenService>()
    .AddTransient<IApplicationUserRepository, ApplicationUserRepository>();

builder.Services.AddMassTransit(x =>
{
    x.SetSnakeCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.Load("IdentityService.Application"));

    x.UsingRabbitMq((context, rabbit) =>
    {
        rabbit.Host(RabbitMqSettings.Host, RabbitMqSettings.VirtualHost, host =>
        {
            host.Username(RabbitMqSettings.Username);
            host.Password(RabbitMqSettings.Password);
        });

        rabbit.ReceiveEndpoint(RabbitMqSettings.FileUploadAvatarImageConsumerEndpointName, e =>
        {
            e.ConfigureConsumer<AddAvatarToUserConsumer>(context);
        });
    });

});

var app = builder.Build();
app.UsePathBase(new PathString("/identity-service"));
app.UseRouting();
app.UseJwksDiscovery();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<MultitenancyMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseIdentitySeeding(builder.Configuration);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapControllers();
app.Run();
