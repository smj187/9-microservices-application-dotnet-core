using BuildingBlocks.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Sets
{
    public interface ISetRepository : IMongoRepository<Set>
    {

    }
}
