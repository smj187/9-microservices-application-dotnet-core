using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Contracts.v1.Contracts
{
    public record AddItemToBasketRequest(Guid ItemId, string ItemName, string? ItemImage, decimal Price, int Quantity);
    public record RemoveItemFromBasketRequest(Guid ItemId);

   
    public record BasketResponse(Guid Id, Guid? UserId, List<ItemResponse> Items, int TotalItems, decimal TotalPrice, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt);
    public record ItemResponse(Guid ItemId, string ItemName, string? ItemImage, decimal Price, int Quantity);
}
