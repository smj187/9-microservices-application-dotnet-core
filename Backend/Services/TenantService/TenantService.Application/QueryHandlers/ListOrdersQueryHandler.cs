using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Queries;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.QueryHandlers
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, IReadOnlyCollection<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IReadOnlyCollection<Order>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.ListAsync();
        }
    }
}
