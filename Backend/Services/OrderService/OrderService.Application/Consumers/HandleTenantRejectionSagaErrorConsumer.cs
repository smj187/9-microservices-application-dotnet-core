using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public class HandleTenantRejectionSagaErrorCommand
    {
        public Guid CorrelationId { get; set; }
        public string TenantId { get; set; } = default!;
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }
    public class HandleTenantRejectionSagaErrorConsumer : IConsumer<HandleTenantRejectionSagaErrorCommand>
    {
        private readonly IConfiguration _configuration;

        public HandleTenantRejectionSagaErrorConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<HandleTenantRejectionSagaErrorCommand> context)
        {
            Console.WriteLine("TODO: refund payment, undo catalog allocation");

            await Task.CompletedTask;
        }
    }
}
