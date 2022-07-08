using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using CatalogService.Core.Domain.Categories;
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
    public class AddVideoToCategoryConsumer : IConsumer<CategoryVideoUploadResponseEvent>
    {
        private readonly IConfiguration _configuration;

        public AddVideoToCategoryConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<CategoryVideoUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var categoryId = context.Message.CategoryId;
            var assetId = context.Message.VideoId;


            var repository = new CategoryRepository(new MultitenancyService(tenantId, _configuration));

            var category = await repository.FindAsync(categoryId);
            if (category == null)
            {
                throw new AggregateNotFoundException(nameof(Category), categoryId);
            }

            category.AddAssetId(assetId);

            await repository.PatchAsync(categoryId, category);
        }
    }
}
