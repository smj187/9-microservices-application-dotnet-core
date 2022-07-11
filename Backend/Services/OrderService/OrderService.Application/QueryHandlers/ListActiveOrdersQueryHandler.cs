using BuildingBlocks.Multitenancy.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Queries;
using OrderService.Core.Entities.Aggregates;
using OrderService.Core.Entities.Enumerations;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.QueryHandlers
{
    public class ListActiveOrdersQueryHandler : IRequestHandler<ListActiveOrdersQuery, IReadOnlyCollection<Order>>
    {
        private readonly IConfiguration _configuration;

        public ListActiveOrdersQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IReadOnlyCollection<Order>> Handle(ListActiveOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderRepository = new OrderRepository(new MultitenancyService(request.TenantId, _configuration));
            return await orderRepository.ListAsync(x => x.OrderStatusValue == 0 || x.OrderStatusValue == 1 || x.OrderStatusValue == 4 || x.OrderStatusValue == 6);
        }
    }
}
