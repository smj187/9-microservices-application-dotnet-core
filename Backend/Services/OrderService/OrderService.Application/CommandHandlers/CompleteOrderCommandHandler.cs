using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Commands;
using OrderService.Core.Entities.Aggregates;
using OrderService.Core.Entities.Enumerations;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CommandHandlers
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand>
    {
        private readonly IConfiguration _configuration;

        public CompleteOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Unit> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRepository = new OrderRepository(new MultitenancyService(request.TenantId, _configuration));
            var order = await orderRepository.FindAsync(request.OrderId);
            if (order == null)
            {
                throw new AggregateNotFoundException(nameof(Order), request.OrderId);
            }

            var a = OrderStatus.OrderComplete;
            var b = OrderStatus.Create(10);
            var c = a == b;
            var d = a.Equals(b);

            order.ChangeOrderStatus(OrderStatus.OrderComplete);

            await orderRepository.PatchAsync(order);

            return Unit.Value;
        }
    }
}
