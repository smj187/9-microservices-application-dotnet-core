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
    public class ListCategoryQueryHandler : IRequestHandler<ListCategoryQuery, IEnumerable<Category>>
    {
        private readonly IMongoRepository<Category> _mongoRepository;

        public ListCategoryQueryHandler(IMongoRepository<Category> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Category>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.ListAsync();
        }
    }
}
