using BuildingBlocks.Domain.Mongo;
using CatalogService.Core.Domain.Set;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class SetRepository : MongoRepository<Set>, ISetRepository
    {
        public SetRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
    }
}
