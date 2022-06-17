using BuildingBlocks.MassTransit.Commands;
using CatalogService.Contracts.v1.Events;
using CatalogService.Core.Domain.Product;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class CatalogConsumer : IConsumer<ItemAllocationCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CatalogConsumer(IProductRepository productRepository, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }

        private static List<Product>? GetReservedProducts(List<Product> items)
        {
            var reserved = new List<Product>();

            foreach (var item in items)
            {
                // if quantity is less than 0, a reservation is not possible
                if(!item.DecreaseQuantity())
                {
                    return null;
                }

                reserved.Add(item);
            }

            return reserved;
        }


        public async Task Consume(ConsumeContext<ItemAllocationCommand> context)
        {
            var products = await _productRepository.ListAsync(context.Message.Items);
            var productsWithQuantityLimitation = products.Where(x => x.Quantity != null).ToList();


            var invisibleItems = productsWithQuantityLimitation
                .Where(x => !x.IsVisible)
                .ToList();

            if (invisibleItems.Any())
            {
                var items = invisibleItems
                    .Select(x => new ItemAllocationInformation(x.Id, x.Name, x.Price, x.IsVisible, x.Quantity))
                    .ToList();

                await _publishEndpoint.Publish(new CatalogAllocationNotVisibleEvent(context.Message.CorrelationId, context.Message.OrderId, items, "one or more items are not visible"));
            }



            var reservedProducts = GetReservedProducts(productsWithQuantityLimitation);

            if (reservedProducts == null)
            {
                // get failed allocated items
                var items = products
                    .Where(x => x.Quantity == 0)
                    .Select(x => new ItemAllocationInformation(x.Id, x.Name, x.Price, x.IsVisible, x.Quantity))
                    .ToList();

                await _publishEndpoint.Publish(new CatalogAllocationOutOfStockEvent(context.Message.CorrelationId, context.Message.OrderId, items, "one or more items are out of stock"));
            }
            else
            {
                // update quantity
                await _productRepository.UpdateMultipleQuantities(reservedProducts);

                var items = reservedProducts
                    //.Select(x => new ItemAllocationInformation(x.Id, x.Name, x.Price, x.IsVisible, x.Quantity))
                    .Select(x => new ItemAllocationInformation(x.Id, x.Name, x.Price, x.IsVisible, 1))
                    .ToList();

                await _publishEndpoint.Publish(new CatalogAllocationSuccessEvent(context.Message.CorrelationId, context.Message.OrderId, items));
            }
        }
    }
}
