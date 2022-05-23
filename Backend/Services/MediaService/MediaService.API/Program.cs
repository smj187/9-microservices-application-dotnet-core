using MediaService.API.Extensions;
using MediaService.Application.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IMediaFileRepository, MediaFileRepository>();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();
await app.UseInitialMigration();
app.UseDevEnvironment();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
