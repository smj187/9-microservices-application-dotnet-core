using BuildingBlocks.Mongo;
using CatalogService.Application.Commands.Categories;
using CatalogService.Core.Entities.Aggregates;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Categories
{
    public class PatchCategoryVisibilityCommandHandler : IRequestHandler<PatchCategoryVisibilityCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public PatchCategoryVisibilityCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Handle(PatchCategoryVisibilityCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(x => x.Id == request.CategoryId);

            if (category == null)
            {
                throw new NotImplementedException();
            }

            category.ChangeVisibility(request.IsVisible);

            return await _categoryRepository.PatchAsync(request.CategoryId, category);
        }
    }
}
