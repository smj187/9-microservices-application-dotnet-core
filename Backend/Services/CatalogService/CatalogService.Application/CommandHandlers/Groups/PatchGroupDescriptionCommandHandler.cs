using BuildingBlocks.Exceptions;
using CatalogService.Application.Commands.Groups;
using CatalogService.Core.Domain.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Groups
{
    public class PatchGroupDescriptionCommandHandler : IRequestHandler<PatchGroupDescriptionCommand, Group>
    {
        private readonly IGroupRepository _groupRepository;

        public PatchGroupDescriptionCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> Handle(PatchGroupDescriptionCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.FindAsync(request.GroupId);

            if (group == null)
            {
                throw new AggregateNotFoundException(nameof(Group), request.GroupId);
            }


            group.ChangeDescription(request.Name, request.Description, request.PriceDescription, request.Tags);

            return await _groupRepository.PatchAsync(request.GroupId, group);
        }
    }
}
