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
        public ListDeliveriesQueryHandler()
        {
        }

        public Task<IEnumerable<Delivery>> Handle(ListDeliveriesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
