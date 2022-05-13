using MongoDB.Bson;
using MediatR;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using CatalogService.Application.Queries;
using CatalogService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });


builder.Services.AddMediatR(typeof(ListProductsQuery));
builder.Services.AddMediatR(typeof(ListCategoriesQuery));

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

builder.Services.AddTransient<ICatalogContext>(opts =>
{
    var str = builder.Configuration.GetConnectionString("MongoDB");
    var database = builder.Configuration.GetConnectionString("Database");
    return new CatalogContext(str, database);
});




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
