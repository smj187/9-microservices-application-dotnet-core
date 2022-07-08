using BuildingBlocks.Mongo.Repositories;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using CatalogService.Core.Domain.Sets;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class SetRepository : MongoRepository<Set>, ISetRepository
    {
        public SetRepository(IMultitenancyService multitenancyService)
            : base(multitenancyService.GetConnectionString(), $"catalog_{multitenancyService.GetTenantId()}")
        {

        }

    }
}
