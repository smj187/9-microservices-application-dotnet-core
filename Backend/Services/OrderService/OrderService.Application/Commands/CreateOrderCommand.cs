using MediatR;
using OrderService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Order NewOrder { get; set; } = default!;
    }
}
