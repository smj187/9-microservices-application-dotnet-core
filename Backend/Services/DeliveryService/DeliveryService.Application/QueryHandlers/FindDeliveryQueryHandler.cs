using BuildingBlocks.Exceptions.Domain;
using DeliveryService.Application.Queries;
using DeliveryService.Core.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Application.QueryHandlers
{
    public class FindDeliveryQueryHandler : IRequestHandler<FindDeliveryQuery, Delivery>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public FindDeliveryQueryHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<Delivery> Handle(FindDeliveryQuery request, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.FindAsync(request.DeliveryId);
            if (delivery == null)
            {
                throw new AggregateNotFoundException(nameof(Delivery), request.DeliveryId);
            }

            return delivery;
        }
    }
}
