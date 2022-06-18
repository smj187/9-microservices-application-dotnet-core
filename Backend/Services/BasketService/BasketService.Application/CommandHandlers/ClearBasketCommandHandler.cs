using BasketService.Application.Commands;
using BasketService.Core.Domain;
using BuildingBlocks.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.CommandHandlers
{
    public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommand, Basket>
    {
        private readonly IBasketRepository _basketRepository;

        public ClearBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.FindAsync(request.BasketId);
            if (basket == null)
            {
                throw new AggregateNotFoundException(nameof(Basket), request.BasketId);
            }

            basket.ClearCart();

            return await _basketRepository.PatchAsync(request.BasketId, basket);
        }
    }
}
