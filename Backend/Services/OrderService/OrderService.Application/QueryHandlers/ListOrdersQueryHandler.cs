using MediatR;
using OrderService.Application.Queries;
using OrderService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.QueryHandlers
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, IEnumerable<Order>>
    {
        public ListOrdersQueryHandler()
        {

        }

        public Task<IEnumerable<Order>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
