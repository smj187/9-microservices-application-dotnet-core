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
    public class ListGroupsQueryHandler : IRequestHandler<ListGroupsQuery, IReadOnlyCollection<Group>>
    {
        private readonly IGroupRepository _groupRepository;

        public ListGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IReadOnlyCollection<Group>> Handle(ListGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _groupRepository.ListAsync();
        }
    }
}
