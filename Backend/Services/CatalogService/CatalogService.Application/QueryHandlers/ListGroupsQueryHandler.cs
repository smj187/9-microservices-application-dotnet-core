using BuildingBlocks.Mongo;
using CatalogService.Application.Queries;
using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers
{
    public class ListGroupsQueryHandler : IRequestHandler<ListGroupsQuery, IEnumerable<Group>>
    {
        private readonly IMongoRepository<Group> _mongoRepository;

        public ListGroupsQueryHandler(IMongoRepository<Group> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Group>> Handle(ListGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.ListAsync();
        }
    }
}
