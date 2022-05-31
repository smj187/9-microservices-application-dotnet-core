using BuildingBlocks.Mongo;
using CatalogService.Application.Queries.Groups;
using CatalogService.Core.Entities.Aggregates;
using CatalogService.Infrastructure.Repositories;
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
            return await _groupRepository.FindAsync(request.GroupId);
        }
    }
}
