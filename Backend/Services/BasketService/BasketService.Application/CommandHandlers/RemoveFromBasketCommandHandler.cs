using BasketService.Application.Commands;
using BasketService.Core.Domain;
using BuildingBlocks.Exceptions.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.CommandHandlers
{
    public class RemoveFromBasketCommandHandler : IRequestHandler<RemoveFromBasketCommand, Basket>
    {
        private readonly IBasketRepository _basketRepository;

        public RemoveFromBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> Handle(RemoveFromBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.FindAsync(request.BasketId);
            if (basket == null)
            {
                throw new AggregateNotFoundException(nameof(Basket), request.BasketId);
            }

            var validTypes = new List<string> { "product", "set" };
            if (!validTypes.Contains(request.Type))
            {
                throw new DomainViolationException($"type of {request.Type} is not allowed");
            }

            if (request.Type == "product")
            {
                basket.RemoveProduct(request.Id);
            }
            else
            {
                basket.RemoveSet(request.Id);
            }


            return await _basketRepository.PatchAsync(request.BasketId, basket);
        }
    }
}
