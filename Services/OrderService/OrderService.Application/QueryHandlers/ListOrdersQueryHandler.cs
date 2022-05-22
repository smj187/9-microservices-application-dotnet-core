using MediatR;
using OrderService.Application.Queries;
using OrderService.Application.Repositories;
using OrderService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.QueryHandlers
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.ListOrderAsync();
        }
    }
}
