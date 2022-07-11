using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class CompleteOrderCommand : IRequest
    {
        public string TenantId { get; set; } = default!;
        public Guid OrderId { get; set; }
    }
}
