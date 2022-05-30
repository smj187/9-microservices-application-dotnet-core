using CatalogService.Contracts.v1.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Consumers
{
    public class PubishEventConsumer : IConsumer<PublishEvent>
    {
        private readonly ILogger<PubishEventConsumer> _logger;

        public PubishEventConsumer(ILogger<PubishEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PublishEvent> context)
        {
            var data = context.Message;

            _logger.LogInformation("AAA", data.Message);

            await Task.CompletedTask;
        }
    }
}
