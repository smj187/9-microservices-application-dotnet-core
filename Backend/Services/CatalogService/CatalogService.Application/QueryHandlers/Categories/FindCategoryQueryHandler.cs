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
    public class FindCategoryQueryHandler : IRequestHandler<FindCategoryQuery, Category>
    {
        private readonly IMongoRepository<Category> _categoryRepository;
        private readonly IMongoRepository<Product> _productRepository;

        public FindCategoryQueryHandler(IMongoRepository<Category> categoryRepository, IMongoRepository<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<Category> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.FindAsync(request.CategoryId);
        }
    }
}
