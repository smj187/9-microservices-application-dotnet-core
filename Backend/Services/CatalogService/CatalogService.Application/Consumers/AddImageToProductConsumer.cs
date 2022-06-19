using BuildingBlocks.Exceptions.Domain;
using CatalogService.Core.Domain.Products;
using FileService.Contracts.v1.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class AddImageToProductConsumer : IConsumer<ProductImageUploadResponseEvent>
    {
        private readonly IProductRepository _productRepository;

        public AddImageToProductConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<ProductImageUploadResponseEvent> context)
        {
            var productId = context.Message.ProductId;
            var assetId = context.Message.ImageId;

            var product = await _productRepository.FindAsync(productId);
            if (product == null)
            {
                throw new AggregateNotFoundException(nameof(Product), productId);
            }

            product.AddAssetId(assetId);

            await _productRepository.PatchAsync(productId, product);
        }
    }
}
