using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Contracts.v1.Commands;
using TenantService.Contracts.v1.Events;
using TenantService.Core.Domain.Aggregates;
using TenantService.Infrastructure.Data;
using TenantService.Infrastructure.Repositories;

namespace TenantService.Application.Consumers
{
    public class TenantSagaConsumer : IConsumer<TenantApprovalCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public TenantSagaConsumer(IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
            _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<TenantApprovalCommand> context)
        {
            var tenantId = context.Message.TenantId;
            var accepted = true;


            var optionsBuilder = new DbContextOptionsBuilder<TenantContext>();
            var orderContext = new TenantContext(optionsBuilder.Options, _configuration, new MultitenancyService(tenantId, _configuration));
            var orderRepository = new OrderRepository(orderContext);

            var order = new Order(tenantId, context.Message.UserId, context.Message.CorrelationId, context.Message.TotalAmount, context.Message.Products, context.Message.Sets);
            await orderContext.Orders.AddAsync(order);
            await orderContext.SaveChangesAsync();

            Console.WriteLine($"{nameof(TenantSagaConsumer)}:: waiting for tenant to approve {context.Message.Products.Count() + context.Message.Sets.Count()} items..");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine($"{nameof(TenantSagaConsumer)}:: tenant {(accepted ? "approved" : "declined")}");

            if (accepted)
            {
                order.AcceptOrder();
                await orderContext.SaveChangesAsync();
                await _publishEndpoint.Publish(new TenantApproveOrderSagaEvent(context.Message.CorrelationId));
            }
            else
            {
                order.RejectOrder();
                await orderContext.SaveChangesAsync();
                await _publishEndpoint.Publish(new TenantRejectOrderSagaEvent(context.Message.CorrelationId));
            }

           
        }
    }
}
