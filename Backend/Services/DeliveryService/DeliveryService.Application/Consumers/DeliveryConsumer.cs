using BuildingBlocks.MassTransit.Commands;
using DeliveryService.Contracts.v1.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Application.Consumers
{
    public class DeliveryConsumer : IConsumer<DeliveryCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public DeliveryConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<DeliveryCommand> context)
        {
            var isDelivered = true;

            Console.WriteLine($"{nameof(DeliveryConsumer)}:: checking if {context.Message.Items.Count} product are in stock");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine($"{nameof(DeliveryConsumer)}:: checkin complete - products {(isDelivered ? "" : "not")} in stock");

            if (isDelivered)
            {
                await _publishEndpoint.Publish(new DeliverySuccessEvent(context.Message.CorrelationId, context.Message.OrderId, "success"));
            }
            else
            {
                await _publishEndpoint.Publish(new DeliveryFailureEvent(context.Message.CorrelationId, context.Message.OrderId, "failure"));
            }

            await Task.CompletedTask;
        }
    }
}
