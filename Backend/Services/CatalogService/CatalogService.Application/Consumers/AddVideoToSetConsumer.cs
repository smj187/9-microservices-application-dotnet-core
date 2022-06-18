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
    public class AddVideoToSetConsumer : IConsumer<SetVideoUploadResponseEvent>
    {
        private readonly ISetRepository _setRepository;

        public AddVideoToSetConsumer(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task Consume(ConsumeContext<SetVideoUploadResponseEvent> context)
        {
            var setId = context.Message.SetId;
            var assetId = context.Message.VideoId;

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
