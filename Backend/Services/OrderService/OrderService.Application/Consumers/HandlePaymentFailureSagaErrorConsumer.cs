using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public class HandlePaymentFailureSagaErrorCommand
    {
        public Guid CorrelationId { get; set; }
        public string TenantId { get; set; } = default!;
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }

    public class HandlePaymentFailureSagaErrorConsumer : IConsumer<HandlePaymentFailureSagaErrorCommand>
    {
        private readonly IConfiguration _configuration;

        public HandlePaymentFailureSagaErrorConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<HandlePaymentFailureSagaErrorCommand> context)
        {
            Console.WriteLine("TODO: undo catalog allocation");

            await Task.CompletedTask;
        }
    }
}
