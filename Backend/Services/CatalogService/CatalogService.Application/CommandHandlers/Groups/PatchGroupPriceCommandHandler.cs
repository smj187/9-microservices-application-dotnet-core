using BuildingBlocks.Mongo;
using CatalogService.Application.Commands.Groups;
using CatalogService.Core.Entities.Aggregates;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Groups
{
    public class PatchGroupPriceCommandHandler : IRequestHandler<PatchGroupPriceCommand, Group>
    {
        private readonly IGroupRepository _groupRepository;

        public PatchGroupPriceCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> Handle(PatchGroupPriceCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.FindAsync(x => x.Id == request.GroupId);

            if (group == null)
            {
                throw new NotImplementedException();
            }


            group.ChangePrice(request.Price);

            return await _groupRepository.PatchAsync(request.GroupId, group);
        }
    }
}
