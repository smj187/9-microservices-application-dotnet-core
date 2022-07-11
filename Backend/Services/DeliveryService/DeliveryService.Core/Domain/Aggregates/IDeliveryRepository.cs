using BuildingBlocks.EfCore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Core.Domain.Aggregates
{
    public interface IDeliveryRepository : IEfRepository<Delivery>
    {

    }
}
