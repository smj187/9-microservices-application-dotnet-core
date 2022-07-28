using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Queries;
using OrderService.Core.Entities.Aggregates;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.QueryHandlers
{
    public class FindOrderQueryHandler : IRequestHandler<FindOrderQuery, Order>
    {
        private readonly IConfiguration _configuration;

        public FindOrderQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Order> Handle(FindOrderQuery request, CancellationToken cancellationToken)
        {
            var orderRepository = new OrderRepository(new MultitenancyService(request.TenantId, _configuration));
            var order = await orderRepository.FindAsync(request.OrderId);
            if (order == null)
            {
                throw new AggregateNotFoundException(nameof(Order), request.OrderId);
            }

            return order;
        }
    }
}
