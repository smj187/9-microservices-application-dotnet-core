using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Entities.Aggregates;
using OrderService.Core.Entities.Enumerations;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public record CompleteOrderSagaSuccessEvent(Guid CorrelationId, Guid OrderId);

    public record CompleteOrderSagaCommand(Guid CorrelationId, string TenantId, Guid OrderId, Guid UserId, Guid BasketId, List<Guid> Products, List<Guid> Sets, decimal TotalAmount);
    
    public class CompleteOrderSagaConsumer : IConsumer<CompleteOrderSagaCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public CompleteOrderSagaConsumer(IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
            _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<CompleteOrderSagaCommand> context)
        {
            var tenantId = context.Message.TenantId;
            var orderRepository = new OrderRepository(new MultitenancyService(tenantId, _configuration));

            var order = await orderRepository.FindAsync(context.Message.OrderId);
            if (order == null)
            {
                throw new AggregateNotFoundException(nameof(Order), context.Message.OrderId);
            }

            order.ChangeOrderStatus(OrderStatus.OrderComplete);
            await orderRepository.PatchAsync(order);

            await _publishEndpoint.Publish(new CompleteOrderSagaSuccessEvent(context.Message.CorrelationId, context.Message.OrderId));

        }
    }
}
