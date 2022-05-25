using DeliveryService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Application.Commands
{
    public class CreateDeliveryCommand : IRequest<Delivery>
    {
        public Delivery NewDelivery { get; set; } = default!;
    }
}
