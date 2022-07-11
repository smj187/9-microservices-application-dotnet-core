using BuildingBlocks.Mongo.Repositories;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using DeliveryService.Core.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Infrastructure.Repositories
{
    public class DeliveryRepository : MongoRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(IMultitenancyService multitenancyService) 
            : base(multitenancyService.GetConnectionString(), $"delivery_{multitenancyService.GetTenantId()}")
        {

        }
    }
}
