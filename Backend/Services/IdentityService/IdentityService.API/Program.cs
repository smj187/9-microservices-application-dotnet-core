using BuildingBlocks.Extensions;
using BuildingBlocks.MassTransit;
using BuildingBlocks.Middleware;
using IdentityService.API.Extensions;
using IdentityService.API.Middleware;
using IdentityService.Application.Services;
using IdentityService.Infrastructure.Data;
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
    .AddTransient<ITokenService, TokenService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.Load("IdentityService.Application"));
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(RabbitMqSettings.RabbitMqUri);
        config.ConfigureEndpoints(context, new SnakeCaseEndpointNameFormatter("identity", false));
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
app.UseMiddleware<AuthenticationMiddleware>();
app.UseInitialDatabaseSeeding();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapControllers();
app.Run();
