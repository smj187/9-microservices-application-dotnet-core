using MediatR;
using OrderService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Queries
{
    public class ListOrdersQuery : IRequest<IEnumerable<Order>>
    {

    }
}
