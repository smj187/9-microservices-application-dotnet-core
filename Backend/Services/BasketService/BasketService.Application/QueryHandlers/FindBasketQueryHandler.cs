using BasketService.Application.Queries;
using BasketService.Core.Domain;
using BuildingBlocks.Exceptions.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.QueryHandlers
{
    public class FindBasketQueryHandler : IRequestHandler<FindBasketQuery, Basket>
    {
        private readonly IBasketRepository _basketRepository;

        public FindBasketQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> Handle(FindBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.FindAsync(request.BasketId);
            if (basket == null)
            {
                throw new AggregateNotFoundException(nameof(Basket), request.BasketId);
            }

            return basket;
        }
    }
}
