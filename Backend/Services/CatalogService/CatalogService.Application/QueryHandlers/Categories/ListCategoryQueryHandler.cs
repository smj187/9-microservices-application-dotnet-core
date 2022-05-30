using BuildingBlocks.Mongo;
using CatalogService.Application.Queries.Categories;
using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Categories
{
    public class ListCategoryQueryHandler : IRequestHandler<ListCategoryQuery, IReadOnlyCollection<Category>>
    {
        private readonly IMongoRepository<Category> _mongoRepository;

        public ListCategoryQueryHandler(IMongoRepository<Category> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IReadOnlyCollection<Category>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.ListAsync();
        }
    }
}
