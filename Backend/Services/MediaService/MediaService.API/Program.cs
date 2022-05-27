using BuildingBlocks.Extensions;
using MediaService.Core.Entities;
using MediaService.Infrastructure.Data;
using MediaService.Infrastructure.Repositories;
using MediatR;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("MediaService.Application"));
builder.Services.ConfigureMySql<MediaContext>(builder.Configuration)
    .AddTransient<IBlobRepository<Blob>, BlobRepository<Blob>>();


var app = builder.Build();
app.UsePathBase(new PathString("/media-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();