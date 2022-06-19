using CatalogService.Contracts.v1.Commands;
using CatalogService.Contracts.v1.Events;
using CatalogService.Core.Domain.Products;
using CatalogService.Core.Domain.Sets;
using MassTransit;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class CatalogConsumer : IConsumer<CatalogAllocationCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISetRepository _setRepository;

        public CatalogConsumer(IProductRepository productRepository, IPublishEndpoint publishEndpoint, ISetRepository setRepository)
        {
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
            _setRepository = setRepository;
        }


        public async Task Consume(ConsumeContext<CatalogAllocationCommand> context)
        {
            var products = await _productRepository.ListAsync(context.Message.Products);
            var sets = await _setRepository.ListAsync(context.Message.Sets);

            var availableProducts = new List<ItemAllocationInformation>();
            var outOfStockProducts = new List<ItemAllocationInformation>();
            var unavailableProducts = new List<ItemAllocationInformation>();

            foreach (var product in products)
            {
                var p = new ItemAllocationInformation(product.Id, product.Name, product.Price, product.IsVisible);

                if (!product.IsAvailable)
                {
                    unavailableProducts.Add(p);
                }
                else if (!product.DecreaseQuantity())
                {
                    outOfStockProducts.Add(p);
                }
                else
                {
                    availableProducts.Add(p);
                }
            }


            var availableSets = new List<ItemAllocationInformation>();
            var outOfStockSets = new List<ItemAllocationInformation>();
            var unavailableSets = new List<ItemAllocationInformation>();

            foreach (var set in sets)
            {
                var s = new ItemAllocationInformation(set.Id, set.Name, set.Price, set.IsVisible);

                if (!set.IsAvailable)
                {
                    unavailableSets.Add(s);
                }
                else if (!set.DecreaseQuantity())
                {
                    outOfStockSets.Add(s);
                }
                else
                {
                    availableSets.Add(s);
                }
            }

            if (unavailableProducts.Any() || unavailableSets.Any())
            {
                var command = new CatalogAllocationUnavailableErrorSagaEvent(context.Message.CorrelationId, context.Message.OrderId, unavailableProducts, unavailableSets, "available");
                await _publishEndpoint.Publish(command);
            }
            else if (outOfStockProducts.Any() || outOfStockSets.Any())
            {
                var command = new CatalogAllocationOutOfStockErrorSagaEvent(context.Message.CorrelationId, context.Message.OrderId, outOfStockProducts, outOfStockSets, "out of stock");
                await _publishEndpoint.Publish(command);
            }
            else
            {
                await _setRepository.PatchMultipleAsync(GetSetBulk(sets));
                await _productRepository.PatchMultipleAsync(GetProductBulk(products));

                var command = new CatalogAllocationSuccessSagaEvent(context.Message.CorrelationId, context.Message.OrderId, availableProducts, availableSets);
                await _publishEndpoint.Publish(command);
            }
        }

        private List<WriteModel<Set>> GetSetBulk(IReadOnlyCollection<Set> sets)
        {
            var bulk = new List<WriteModel<Set>>();

            foreach (var set in sets)
            {
                var filter = Builders<Set>.Filter.Eq(x => x.Id, set.Id);
                var update = Builders<Set>.Update.Set(x => x.Quantity, set.Quantity);

                var upsert = new UpdateOneModel<Set>(filter, update) { IsUpsert = false };
                bulk.Add(upsert);
            }

            return bulk;
        }

        private List<WriteModel<Product>> GetProductBulk(IReadOnlyCollection<Product> products)
        {
            var bulk = new List<WriteModel<Product>>();

            foreach (var set in products)
            {
                var filter = Builders<Product>.Filter.Eq(x => x.Id, set.Id);
                var update = Builders<Product>.Update.Set(x => x.Quantity, set.Quantity);

                var upsert = new UpdateOneModel<Product>(filter, update) { IsUpsert = false };
                bulk.Add(upsert);
            }

            return bulk;
        }
    }
}
