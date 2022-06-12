using BasketService.Application.Queries;
using BasketService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.QueryHandlers
{
    public class ListBasketsQueryHandler : IRequestHandler<ListBasketsQuery, IReadOnlyCollection<Basket>>
    {
        private readonly IBasketRepository _basketRepository;

        public ListBasketsQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<IReadOnlyCollection<Basket>> Handle(ListBasketsQuery request, CancellationToken cancellationToken)
        {
            return await _basketRepository.ListAsync();
        }
    }
}
