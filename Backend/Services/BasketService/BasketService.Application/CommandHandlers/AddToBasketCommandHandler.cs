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
    public class AddToBasketCommandHandler : IRequestHandler<AddToBasketCommand, Basket>
    {
        private readonly IBasketRepository _basketRepository;

        public AddToBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> Handle(AddToBasketCommand request, CancellationToken cancellationToken)
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
                basket.AddProduct(new Item(request.Id, request.Name, request.Image, request.Price));
            }
            else
            {
                basket.AddSet(new Item(request.Id, request.Name, request.Image, request.Price));
            }


            return await _basketRepository.PatchAsync(request.BasketId, basket);
        }
    }
}
