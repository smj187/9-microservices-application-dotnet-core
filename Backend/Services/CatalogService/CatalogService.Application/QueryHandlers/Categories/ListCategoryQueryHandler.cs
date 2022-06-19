using CatalogService.Application.Queries.Categories;
using CatalogService.Core.Domain.Categories;
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
        private readonly ICategoryRepository _categoryRepository;

        public ListCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyCollection<Category>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.ListAsync();
        }
    }
}
