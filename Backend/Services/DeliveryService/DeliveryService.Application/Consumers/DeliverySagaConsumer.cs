using BuildingBlocks.Multitenancy.Services;
using DeliveryService.Contracts.v1.Commands;
using DeliveryService.Contracts.v1.Events;
using DeliveryService.Core.Domain;
using DeliveryService.Core.Domain.Aggregates;
using DeliveryService.Core.Domain.ValueObjects;
using DeliveryService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Application.Consumers
{
    public class DeliverySagaConsumer : IConsumer<DeliveryCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public DeliverySagaConsumer(IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
            _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<DeliveryCommand> context)
        {
            var tenantId = context.Message.TenantId;
            var success = true;


            var deliveryRepository = new DeliveryRepository(new MultitenancyService(tenantId, _configuration));
            var delivery = new Delivery(tenantId, context.Message.OrderId, context.Message.UserId, context.Message.Products, context.Message.Sets);

            await deliveryRepository.AddAsync(delivery);

            Console.WriteLine($"{nameof(DeliverySagaConsumer)}:: new delivery was created [{delivery.OrderId}]");
            //await Task.Delay(TimeSpan.FromSeconds(1));

            delivery.ChangeDeliveryStatus(DeliveryStatus.Processing);
            await deliveryRepository.PatchAsync(delivery);

            if (success)
            {
                delivery.ChangeDeliveryStatus(DeliveryStatus.Complete);
                await deliveryRepository.PatchAsync(delivery);
                Console.WriteLine($"{nameof(DeliverySagaConsumer)}:: delivery complete");

                await _publishEndpoint.Publish(new DeliverySuccessSagaEvent(context.Message.CorrelationId, context.Message.OrderId, "success"));
            }
            else
            {
                delivery.ChangeDeliveryStatus(DeliveryStatus.Failed);
                await deliveryRepository.PatchAsync(delivery);
                Console.WriteLine($"{nameof(DeliverySagaConsumer)}:: delivery failed");
                await _publishEndpoint.Publish(new DeliveryFailureSagaEvent(context.Message.CorrelationId, context.Message.OrderId, "failure"));
            }
        }
    }
}
