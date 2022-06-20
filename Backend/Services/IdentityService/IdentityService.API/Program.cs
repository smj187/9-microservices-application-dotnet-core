using BuildingBlocks.EfCore.Extensions;
using BuildingBlocks.Masstransit;
using BuildingBlocks.Middleware.Exceptions;
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
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("IdentityService.Application"));

builder.Services.ConfigureIdentityServer();

builder.Services.ConfigureMySql<IdentityContext>(builder.Configuration)
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
//app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseInitialDatabaseSeeding();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapControllers();
app.Run();
