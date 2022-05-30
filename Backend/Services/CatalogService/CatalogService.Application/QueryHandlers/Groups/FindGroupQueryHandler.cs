using BuildingBlocks.Mongo;
using CatalogService.Application.Queries.Groups;
using CatalogService.Core.Entities;
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
        private readonly IMongoRepository<Group> _mongoRepository;

        public FindGroupQueryHandler(IMongoRepository<Group> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Group> Handle(FindGroupQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.FindAsync(request.GroupId);
        }
    }
}
