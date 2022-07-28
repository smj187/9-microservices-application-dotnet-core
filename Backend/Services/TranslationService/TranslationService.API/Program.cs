using BuildingBlocks.Middleware.Exceptions;
using BuildingBlocks.Mongo.Configurations;
using BuildingBlocks.Mongo.Extensions;
using MediatR;
using System.Reflection;
using TranslationService.Core.Aggregates;
using TranslationService.Infrastructure.BsonClassMapDefinitions;
using TranslationService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(Assembly.Load("TranslationService.Application"));

builder.Services.AddMongoDatabase(builder.Configuration)
    .AddTransient<ITranslationRepository, TranslationRepository>()
    .AddEntityBaseMongoConfiguration()
    .AddBsonClassMappings();

var app = builder.Build();
app.UsePathBase(new PathString("/translation-service"));
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
