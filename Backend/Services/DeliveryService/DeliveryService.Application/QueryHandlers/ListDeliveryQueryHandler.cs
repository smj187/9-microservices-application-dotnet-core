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
    public class ListDeliveryQueryHandler : IRequestHandler<ListDeliveryQuery, IReadOnlyCollection<Delivery>>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public ListDeliveryQueryHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<IReadOnlyCollection<Delivery>> Handle(ListDeliveryQuery request, CancellationToken cancellationToken)
        {
            return await _deliveryRepository.ListAsync();
        }
    }
}
