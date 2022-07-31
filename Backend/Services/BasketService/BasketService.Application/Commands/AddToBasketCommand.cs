using BasketService.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Application.Commands
{
    public class AddToBasketCommand : IRequest<Basket>
    {
        public Guid BasketId { get; set; }
        public string Type { get; set; } = default!;

        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Image { get; set; } = null!;

        public decimal Price { get; set; }
        public string TenantId { get; set; } = default!;
    }
}
