using BuildingBlocks.Mongo;
using CatalogService.Application.Queries.Categories;
using CatalogService.Core.Entities;
using CatalogService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Categories
{
    public class FindCategoryQueryHandler : IRequestHandler<FindCategoryQuery, CategoryDetailsModel>
    {
        private readonly IMongoRepository<Category> _categoryRepository;
        private readonly IMongoRepository<Product> _productRepository;

        public FindCategoryQueryHandler(IMongoRepository<Category> categoryRepository, IMongoRepository<Product> productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<CategoryDetailsModel> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(request.CategoryId);

            var t = await _productRepository.FindAsync(category.ProductIds.ToList());
            return new CategoryDetailsModel
            {
                Category = category,
                Products = t.ToList()
            };
        }
    }
}
