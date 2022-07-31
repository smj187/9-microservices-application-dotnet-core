using BasketService.Application.Commands;
using BasketService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.CommandHandlers
{
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, Basket>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = new Basket(request.TenantId, Guid.NewGuid(), request.UserId);

            await _basketRepository.AddAsync(basket);

            return basket;
        }
    }
}
