using MediatR;
using OrderService.Core.Entities.Aggregates;
using OrderService.Core.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public string TenantId { get; set; } = default!;
        public Guid OrderId { get; set; }

        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; } = default!;
    }
}
