using MediatR;
using OrderService.Core.Entities.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries
{
    public class ListCompleteOrdersQuery : IRequest<IReadOnlyCollection<Order>>
    {
        public string TenantId { get; set; } = default!;
    }
}
