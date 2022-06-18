using MassTransit;
using PaymentService.Contracts.v1.Commands;
using PaymentService.Contracts.v1.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Consumers
{
    public class PaymentConsumer : IConsumer<PaymentCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<PaymentCommand> context)
        {
            var success = true;

            Console.WriteLine($"{nameof(PaymentConsumer)}:: payment for {context.Message.OrderId} with {context.Message.Amount}");
            await Task.Delay(1);

            if (success)
            {
                Console.WriteLine($"{nameof(PaymentConsumer)}:: payment success");

                var command = new PaymentSuccessSagaEvent(context.Message.CorrelationId, context.Message.OrderId, "success");
                await _publishEndpoint.Publish(command);
            }
            else
            {
                Console.WriteLine($"{nameof(PaymentConsumer)}:: payment failure");

                var command = new PaymentFailureSagaEvent(context.Message.CorrelationId, context.Message.OrderId, "failure (test)");
                await _publishEndpoint.Publish(command);
            }
        }
    }
}
