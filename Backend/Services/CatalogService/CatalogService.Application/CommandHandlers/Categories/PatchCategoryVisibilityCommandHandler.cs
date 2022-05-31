using BuildingBlocks.Mongo;
using CatalogService.Application.Commands.Categories;
using CatalogService.Core.Entities;
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
        private readonly IMongoRepository<Category> _mongoRepository;

        public PatchCategoryVisibilityCommandHandler(IMongoRepository<Category> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Category> Handle(PatchCategoryVisibilityCommand request, CancellationToken cancellationToken)
        {
            var category = await _mongoRepository.FindAsync(x => x.Id == request.CategoryId);

            if (category == null)
            {
                throw new NotImplementedException();
            }

            category.ChangeVisibility(request.IsVisible);

            return await _mongoRepository.PatchAsync(request.CategoryId, category);
        }
    }
}
