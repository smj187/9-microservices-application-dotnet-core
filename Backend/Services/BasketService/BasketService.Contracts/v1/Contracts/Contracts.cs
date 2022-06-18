using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Contracts.v1.Contracts
{
    public record CreateBasketRequest(Guid UserId);

    public record AddToBasketRequest(Guid Id, string Name, string Image, decimal Price, string Type);
    public record RemoveFromBasketRequest(Guid Id, string Type);

   
    public record BasketResponse(Guid Id, Guid UserId, int TotalItems, decimal TotalPrice, List<ItemResponse> Products, List<ItemResponse> Sets, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt);
    public record ItemResponse(Guid Id, string Name, string Image, decimal Price);
}
