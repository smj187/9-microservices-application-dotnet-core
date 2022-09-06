using CatalogService.Application.Commands.Categories;
using CatalogService.Core.Domain.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.TenantId, Guid.NewGuid(), request.Name, request.Description, request.Products, request.Sets);

            await _categoryRepository.AddAsync(category);

            return category;
        }
    }
}
