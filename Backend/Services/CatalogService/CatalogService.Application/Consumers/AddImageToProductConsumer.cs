using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using CatalogService.Core.Domain.Products;
using CatalogService.Infrastructure.Repositories;
using FileService.Contracts.v1.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class AddImageToProductConsumer : IConsumer<ProductImageUploadResponseEvent>
    {
        private readonly IConfiguration _configuration;

        public AddImageToProductConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<ProductImageUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var productId = context.Message.ProductId;
            var assetId = context.Message.ImageId;


            var repository = new ProductRepository(new MultitenancyService(tenantId, _configuration));

            var product = await repository.FindAsync(productId);
            if (product == null)
            {
                throw new AggregateNotFoundException(nameof(Product), productId);
            }

            product.AddAssetId(assetId);

            await repository.PatchAsync(productId, product);
        }
    }
}
