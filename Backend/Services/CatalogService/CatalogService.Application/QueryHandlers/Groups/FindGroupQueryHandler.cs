using BuildingBlocks.Exceptions;
using CatalogService.Application.Queries.Groups;
using CatalogService.Core.Domain.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Groups
{
    public class FindGroupQueryHandler : IRequestHandler<FindGroupQuery, Group>
    {
        private readonly IGroupRepository _groupRepository;

        public FindGroupQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> Handle(FindGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.FindAsync(request.GroupId);
            if (group == null)
            {
                throw new AggregateNotFoundException(nameof(Group), request.GroupId);
            }

            return group;
        }
    }
}
