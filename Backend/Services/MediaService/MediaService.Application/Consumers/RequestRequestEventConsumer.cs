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
    public class RequestRequestEventConsumerDefinition : ConsumerDefinition<RequestRequestEventConsumer>
    {
        public RequestRequestEventConsumerDefinition()
        {
            EndpointName = "message-service";
        }
    }
    public class RequestRequestEventConsumer : IConsumer<RequestRequestEvent>
    {
        private readonly ILogger<RequestRequestEventConsumer> _logger;

        public RequestRequestEventConsumer(ILogger<RequestRequestEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RequestRequestEvent> context)
        {
            var data = context.Message;

            _logger.LogInformation("got message", data.Id);

            await Task.Delay(1000);

            await context.RespondAsync(new RequestResponseEvent
            {
                Id = data.Id,
                Echo = ":)"
            });
        }
    }
}
