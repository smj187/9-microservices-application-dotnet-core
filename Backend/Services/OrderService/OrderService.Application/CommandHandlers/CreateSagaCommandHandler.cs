using MassTransit;
using MediatR;
using OrderService.Application.Commands;
using OrderService.Application.StateMachines.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CommandHandlers
{
    public class CreateSagaCommandHandler : IRequestHandler<CreateSagaCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateSagaCommandHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(CreateSagaCommand request, CancellationToken cancellationToken)
        {
            var saga = new CreateOrderSagaEvent(request.OrderId, request.BasketId, request.UserId, request.TenantId, request.Products, request.Sets);

            await _publishEndpoint.Publish(saga, cancellationToken);

            return Unit.Value;
        }
    }
}
