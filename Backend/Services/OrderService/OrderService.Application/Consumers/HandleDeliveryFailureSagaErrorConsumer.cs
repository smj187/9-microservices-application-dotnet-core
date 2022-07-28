using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public class HandleDeliveryFailureSagaErrorCommand
    {
        public Guid CorrelationId { get; set; }
        public string TenantId { get; set; } = default!;
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }

    public class HandleDeliveryFailureSagaErrorConsumer : IConsumer<HandleDeliveryFailureSagaErrorCommand>
    {
        private readonly IConfiguration _configuration;

        public HandleDeliveryFailureSagaErrorConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<HandleDeliveryFailureSagaErrorCommand> context)
        {
            Console.WriteLine("TODO: notify tenant");

            await Task.CompletedTask;
        }
    }
}
