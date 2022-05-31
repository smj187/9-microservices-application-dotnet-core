using BuildingBlocks.Mongo;
using CatalogService.Application.Queries.Categories;
using CatalogService.Core.Entities.Aggregates;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Categories
{
    public class FindCategoryQueryHandler : IRequestHandler<FindCategoryQuery, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public FindCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.FindAsync(request.CategoryId);
        }
    }
}
