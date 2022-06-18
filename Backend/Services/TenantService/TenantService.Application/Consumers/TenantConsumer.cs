using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Contracts.v1.Commands;
using TenantService.Contracts.v1.Events;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Consumers
{
    public class TenantConsumer : IConsumer<TenantCommand>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public TenantConsumer(ITenantRepository tenantRepository, IPublishEndpoint publishEndpoint)
        {
            _tenantRepository = tenantRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<TenantCommand> context)
        {
            var accepted = true;

            Console.WriteLine($"{nameof(TenantConsumer)}:: waiting for tenant to approve {context.Message.Items.Count()} items..");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine($"{nameof(TenantConsumer)}:: tenant {(accepted ? "approved" : "declined")}");

            if(accepted)
            {
                await _publishEndpoint.Publish(new TenantApproveOrderSagaEvent(context.Message.CorrelationId, context.Message.OrderId));
            }
            else
            {
                await _publishEndpoint.Publish(new TenantRejectOrderSagaEvent(context.Message.CorrelationId, context.Message.OrderId));
            }
        }
    }
}
