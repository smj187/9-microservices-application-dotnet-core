using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using CatalogService.Core.Domain.Sets;
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
    public class AddVideoToSetConsumer : IConsumer<SetVideoUploadResponseEvent>
    {
        private readonly IConfiguration _configuration;

        public AddVideoToSetConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<SetVideoUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var setId = context.Message.SetId;
            var assetId = context.Message.VideoId;


            var repository = new SetRepository(new MultitenancyService(tenantId, _configuration));

            var set = await repository.FindAsync(setId);
            if (set == null)
            {
                throw new AggregateNotFoundException(nameof(Set), setId);
            }

            set.AddAssetId(assetId);

            await repository.PatchAsync(setId, set);
        }
    }
}
