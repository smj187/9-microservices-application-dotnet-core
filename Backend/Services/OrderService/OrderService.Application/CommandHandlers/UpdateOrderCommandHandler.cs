using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Commands;
using OrderService.Core.Entities.Aggregates;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CommandHandlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IConfiguration _configuration;

        public UpdateOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRepository = new OrderRepository(new MultitenancyService(request.TenantId, _configuration));
            var order = await orderRepository.FindAsync(request.OrderId);
            if (order == null)
            {
                throw new AggregateNotFoundException(nameof(Order), request.OrderId);
            }

            order.ChangeTotalAmount(request.TotalAmount);
            order.ChangeOrderStatus(request.OrderStatus);

            await orderRepository.PatchAsync(order);

            return Unit.Value;
        }
    }
}
