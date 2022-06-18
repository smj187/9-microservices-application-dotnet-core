using BuildingBlocks.Exceptions;
using CatalogService.Core.Domain.Sets;
using FileService.Contracts.v1.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class AddImageToSetConsumer : IConsumer<SetImageUploadResponseEvent>
    {
        private readonly ISetRepository _setRepository;

        public AddImageToSetConsumer(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task Consume(ConsumeContext<SetImageUploadResponseEvent> context)
        {
            var setId = context.Message.SetId;
            var assetId = context.Message.ImageId;

            var set = await _setRepository.FindAsync(setId);
            if (set == null)
            {
                throw new AggregateNotFoundException(nameof(Set), setId);
            }

            set.AddAssetId(assetId);

            await _setRepository.PatchAsync(setId, set);
        }
    }
}
