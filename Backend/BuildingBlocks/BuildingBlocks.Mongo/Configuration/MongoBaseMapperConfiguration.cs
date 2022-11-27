using BuildingBlocks.Domain;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Configuration
{
    public static class MongoBaseMapperConfiguration
    {
        public static IServiceCollection AddEntityBaseMongoConfiguration(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<EntityBase>(x =>
            {
                x.MapProperty(x => x.Id)
                    .SetElementName("_id")
                    .SetIsRequired(true)
                    .SetSerializer(new GuidSerializer(BsonType.String));

                x.MapProperty(x => x.CreatedAt)
                    .SetElementName("created_at")
                    .SetIsRequired(true)
                    .SetSerializer(new DateTimeOffsetSerializer(BsonType.String));

                x.MapProperty(x => x.ModifiedAt)
                    .SetElementName("modified_at")
                    .SetIsRequired(false)
                    .SetSerializer(new NullableSerializer<DateTimeOffset>().WithSerializer(new DateTimeOffsetSerializer(BsonType.String)));

                x.MapProperty(x => x.IsDeleted)
                    .SetElementName("is_deleted")
                    .SetIsRequired(true)
                    .SetDefaultValue(false);
            });


            return services;
        }
    }
}
