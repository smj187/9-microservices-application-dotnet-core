using MediatR;
using OrderService.Application.Queries;
using OrderService.Core.Entities;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.QueryHandlers
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrderRepository<Order> _orderRepository;

        public ListOrdersQueryHandler(IOrderRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.ListAsync();
        }
    }
}
