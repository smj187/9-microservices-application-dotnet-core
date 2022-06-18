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
    public class PatchCategoryDescriptionCommandHandler : IRequestHandler<PatchCategoryDescriptionCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public PatchCategoryDescriptionCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Handle(PatchCategoryDescriptionCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(request.CategoryId);

            if (category == null)
            {
                throw new AggregateNotFoundException(nameof(Category), request.CategoryId);
            }


            category.ChangeDescription(request.Name, request.Description);

            return await _categoryRepository.PatchAsync(request.CategoryId, category);
        }
    }
}
