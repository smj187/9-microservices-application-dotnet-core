using BuildingBlocks.Multitenancy.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Commands;
using OrderService.Core.Entities.Aggregates;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IConfiguration _configuration;

        public CreateOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRepository = new OrderRepository(new MultitenancyService(request.TenantId, _configuration));

            var order = new Order(request.TenantId, request.OrderId, request.UserId, request.Products, request.Sets);

            await orderRepository.AddAsync(order);

            return Unit.Value;
        }
    }
}
