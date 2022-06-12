using BasketService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.Commands
{
    public class AddItemToBasketCommand : IRequest<Basket>
    {
        public Guid BasketId { get; set; }


        public Guid ItemId { get; set; }
        public string ItemName { get; set; } = default!;
        public string? ItemImage { get; set; } = null!;

        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
