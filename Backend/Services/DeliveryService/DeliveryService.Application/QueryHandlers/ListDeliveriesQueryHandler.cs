using BuildingBlocks.Mongo;
using DeliveryService.Application.Queries;
using DeliveryService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Application.QueryHandlers
{
    public class ListDeliveriesQueryHandler : IRequestHandler<ListDeliveriesQuery, IEnumerable<Delivery>>
    {
        private readonly IMongoRepository<Delivery> _mongoRepository;

        public ListDeliveriesQueryHandler(IMongoRepository<Delivery> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Delivery>> Handle(ListDeliveriesQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.ListAsync();
        }
    }
}
