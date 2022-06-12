using BasketService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.Queries
{
    public class FindBasketQuery : IRequest<Basket>
    {
        public Guid BasketId { get; set; }
    }
}
