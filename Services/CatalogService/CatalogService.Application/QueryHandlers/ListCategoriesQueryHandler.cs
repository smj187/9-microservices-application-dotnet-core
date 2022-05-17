using CatalogService.Application.Queries;
using CatalogService.Application.Repositories.Categories;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers
{
    public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryQueryRepository _categoryQueryRepository;

        public ListCategoriesQueryHandler(ICategoryQueryRepository categoryQueryRepository)
        {
            _categoryQueryRepository = categoryQueryRepository;
        }

        public async Task<IEnumerable<Category>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryQueryRepository.ListAsync();
        }
    }
}
