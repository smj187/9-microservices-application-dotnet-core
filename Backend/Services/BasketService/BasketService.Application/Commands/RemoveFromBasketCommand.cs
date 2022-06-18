using BasketService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.Commands
{
    public class RemoveFromBasketCommand : IRequest<Basket>
    {
        public Guid BasketId { get; set; }
        public string Type { get; set; } = default!;

        public Guid Id { get; set; }
    }
}
