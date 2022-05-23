using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Entities.Base
{
    public abstract class MongoEntity : IMongoEntity
    {
        public Guid Id { get; set; }
    }
}
