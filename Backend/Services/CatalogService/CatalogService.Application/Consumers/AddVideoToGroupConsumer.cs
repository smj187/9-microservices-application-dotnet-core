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
    public class AddVideoToGroupConsumer : IConsumer<GroupVideoUploadResponseEvent>
    {
        private readonly IGroupRepository _groupRepository;

        public AddVideoToGroupConsumer(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task Consume(ConsumeContext<GroupVideoUploadResponseEvent> context)
        {
            var groupId = context.Message.GroupId;
            var assetId = context.Message.VideoId;

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
