using BuildingBlocks.Multitenancy.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentService.Contracts.v1.Commands;
using PaymentService.Contracts.v1.Events;
using PaymentService.Core.Domain.Aggregates;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Consumers
{
    public class PaymentSagaConsumer : IConsumer<PaymentCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public PaymentSagaConsumer(IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
            _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<PaymentCommand> context)
        {
            var tenantId = context.Message.TenantId;
            var success = true;

            Console.WriteLine($"{nameof(PaymentSagaConsumer)}:: payment for {context.Message.OrderId} with {context.Message.Amount}");

            if (success)
            {
                Console.WriteLine($"{nameof(PaymentSagaConsumer)}:: payment success");

                var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
                var paymentContext = new PaymentContext(optionsBuilder.Options, _configuration, new MultitenancyService(tenantId, _configuration));

                var payment = new Payment(tenantId, context.Message.UserId, context.Message.OrderId, context.Message.Amount, context.Message.Products, context.Message.Sets);

                await paymentContext.Payments.AddAsync(payment);
                await paymentContext.SaveChangesAsync();


                var command = new PaymentSuccessSagaEvent(context.Message.CorrelationId, context.Message.OrderId, "success");
                await _publishEndpoint.Publish(command);
            }
            else
            {
                Console.WriteLine($"{nameof(PaymentSagaConsumer)}:: payment failure");

                var command = new PaymentFailureSagaEvent(context.Message.CorrelationId, context.Message.OrderId, "failure (test)");
                await _publishEndpoint.Publish(command);
            }
        }
    }
}
