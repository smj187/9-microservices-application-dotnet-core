using BuildingBlocks.Exceptions;
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
    public class RemoveSetFromCategoryCommandHandler : IRequestHandler<RemoveSetFromCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public RemoveSetFromCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Handle(RemoveSetFromCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(request.CategoryId);

            if (category == null)
            {
                throw new AggregateNotFoundException(nameof(Category), request.CategoryId);
            }

            category.RemoveSet(request.SetId);

            return await _categoryRepository.PatchAsync(request.CategoryId, category);
        }
    }
}
