using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public class HandleCatalogUnavailableSagaErrorCommand
    {
        public Guid CorrelationId { get; set; }
        public string TenantId { get; set; } = default!;
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }

    public class HandleCatalogUnavailableSagaErrorConsumer : IConsumer<HandleCatalogUnavailableSagaErrorCommand>
    {
        private readonly IConfiguration _configuration;

        public HandleCatalogUnavailableSagaErrorConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<HandleCatalogUnavailableSagaErrorCommand> context)
        {
            var products = context.Message.Products.Select(x => x.ToString());
            var sets = context.Message.Sets.Select(x => x.ToString());
            Console.WriteLine($"-- unavailable -> products: {string.Join(", ", products)}");
            Console.WriteLine($"-- unavailable -> sets: {string.Join(", ", sets)}");

            await Task.CompletedTask;
        }
    }
}
