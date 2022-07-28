using DeliveryService.Core.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Application.Queries
{
    public class FindDeliveryQuery : IRequest<Delivery>
    {
        public Guid DeliveryId { get; set; }
    }
}
