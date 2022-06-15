using BuildingBlocks.Exceptions;
using CatalogService.Core.Domain.Product;
using FileService.Contracts.v1.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class AddVideoToProductConsumer : IConsumer<ProductVideoUploadResponseEvent>
    {
        private readonly IProductRepository _productRepository;

        public AddVideoToProductConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<ProductVideoUploadResponseEvent> context)
        {
            var productId = context.Message.ProductId;
            var assetId = context.Message.VideoId;

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
