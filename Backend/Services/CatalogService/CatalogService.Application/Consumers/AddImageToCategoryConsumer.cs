using BuildingBlocks.Exceptions;
using CatalogService.Core.Domain.Category;
using FileService.Contracts.v1.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class AddImageToCategoryConsumer : IConsumer<CategoryImageUploadResponseEvent>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddImageToCategoryConsumer(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Consume(ConsumeContext<CategoryImageUploadResponseEvent> context)
        {
            var categoryId = context.Message.CategoryId;
            var assetId = context.Message.ImageId;

            var category = await _categoryRepository.FindAsync(categoryId);
            if (category == null)
            {
                throw new AggregateNotFoundException(nameof(Category), categoryId);
            }

            category.AddAssetId(assetId);

            await _categoryRepository.PatchAsync(categoryId, category);
        }
    }
}
