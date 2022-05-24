using BuildingBlocks.Mongo;
using DeliveryService.Application.Commands;
using DeliveryService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Application.CommandHandlers
{
    public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand, Delivery>
    {
        private readonly IMongoRepository<Delivery> _mongoRepository;

        public CreateDeliveryCommandHandler(IMongoRepository<Delivery> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Delivery> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.AddAsync(request.NewDelivery);
        }
    }
}
