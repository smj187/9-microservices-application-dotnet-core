using BuildingBlocks.Multitenancy.Services;
using CatalogService.Contracts.v1.Commands;
using CatalogService.Contracts.v1.Events;
using CatalogService.Core.Domain.Products;
using CatalogService.Core.Domain.Sets;
using CatalogService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class CatalogSagaConsumer : IConsumer<CatalogSagaAllocationCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IConfiguration _configuration;

        public CatalogSagaConsumer(IPublishEndpoint publishEndpoint, IConfiguration configuration)
        {
            _publishEndpoint = publishEndpoint;
            _configuration = configuration;
        }


        public async Task Consume(ConsumeContext<CatalogSagaAllocationCommand> context)
        {
            var tenantId = context.Message.TenantId;

            var setRepository = new SetRepository(new MultitenancyService(tenantId, _configuration));
            var productRepository = new ProductRepository(new MultitenancyService(tenantId, _configuration));

            var products = await productRepository.ListAsync(context.Message.Products);
            var sets = await setRepository.ListAsync(context.Message.Sets);

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

            await Task.Delay(1000);

            if (unavailableProducts.Any() || unavailableSets.Any())
            {
                var command = new CatalogAllocationUnavailableErrorSagaEvent(context.Message.CorrelationId, context.Message.OrderId, unavailableProducts, unavailableSets, "available");
                Console.WriteLine("unavailable products or sets");
                await _publishEndpoint.Publish(command);
            }
            else if (outOfStockProducts.Any() || outOfStockSets.Any())
            {
                var command = new CatalogAllocationOutOfStockErrorSagaEvent(context.Message.CorrelationId, context.Message.OrderId, outOfStockProducts, outOfStockSets, "out of stock");
                Console.WriteLine("out of stock products or sets");
                await _publishEndpoint.Publish(command);
            }
            else
            {
                await setRepository.PatchMultipleAsync(GetSetBulk(sets));
                await productRepository.PatchMultipleAsync(GetProductBulk(products));

                var command = new CatalogAllocationSuccessSagaEvent(context.Message.CorrelationId, context.Message.OrderId, availableProducts, availableSets);
                Console.WriteLine("all good for products and sets");
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
