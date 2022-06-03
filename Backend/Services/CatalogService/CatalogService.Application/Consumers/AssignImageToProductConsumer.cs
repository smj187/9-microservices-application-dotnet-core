using BuildingBlocks.Exceptions;
using CatalogService.Core.Domain.Product;
using FileService.Contracts.v1;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class AssignImageToProductConsumer : IConsumer<AddImageToProductResponseEvent>
    {
        private readonly IProductRepository _productRepository;

        public AssignImageToProductConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<AddImageToProductResponseEvent> context)
        {
            var productId = context.Message.ProductId;
            var imageId = context.Message.ImageId;

            var product = await _productRepository.FindAsync(productId);

            if (product == null)
            {
                throw new AggregateNotFoundException(nameof(Product), productId);
            }

            product.AddImageId(imageId);

            await _productRepository.PatchAsync(productId, product);
        }
    }
}
