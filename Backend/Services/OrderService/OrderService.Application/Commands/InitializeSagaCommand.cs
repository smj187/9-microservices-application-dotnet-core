using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Commands
{
    public class InitializeSagaCommand : IRequest
    {
        public string TenantId { get; set; } = default!;
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid BasketId { get; set; }
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }
}
