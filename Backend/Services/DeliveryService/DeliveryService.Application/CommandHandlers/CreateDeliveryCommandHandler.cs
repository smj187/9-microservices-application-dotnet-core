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

        public CreateDeliveryCommandHandler()
        {
            
        }

        public Task<Delivery> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
