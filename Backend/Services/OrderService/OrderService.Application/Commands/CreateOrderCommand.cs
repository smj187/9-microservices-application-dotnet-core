using MediatR;
using OrderService.Core.Entities.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public string TenantId { get; set; } = default!;
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }
}
