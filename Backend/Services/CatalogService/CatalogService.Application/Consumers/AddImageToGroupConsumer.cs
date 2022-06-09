using BuildingBlocks.Exceptions;
using CatalogService.Core.Domain.Group;
using FileService.Contracts.v1.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Consumers
{
    public class AddImageToGroupConsumer : IConsumer<GroupImageUploadResponseEvent>
    {
        private readonly IGroupRepository _groupRepository;

        public AddImageToGroupConsumer(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task Consume(ConsumeContext<GroupImageUploadResponseEvent> context)
        {
            var groupId = context.Message.GroupId;
            var assetId = context.Message.ImageId;

            var group = await _groupRepository.FindAsync(groupId);
            if (group == null)
            {
                throw new AggregateNotFoundException(nameof(Group), groupId);
            }

            group.AddAssetId(assetId);

            await _groupRepository.PatchAsync(groupId, group);
        }
    }
}
