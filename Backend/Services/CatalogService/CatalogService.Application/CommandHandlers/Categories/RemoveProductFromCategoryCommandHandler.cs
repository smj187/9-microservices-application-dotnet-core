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
    public class RemoveProductFromCategoryCommandHandler : IRequestHandler<RemoveProductFromCategoryCommand, Category>
    {
        private readonly IMongoRepository<Category> _mongoRepository;

        public RemoveProductFromCategoryCommandHandler(IMongoRepository<Category> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Category> Handle(RemoveProductFromCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _mongoRepository.FindAsync(x => x.Id == request.CategoryId);

            if (category == null)
            {
                throw new NotImplementedException();
            }

            category.RemoveProduct(request.ProductId);

            return await _mongoRepository.PatchAsync(request.CategoryId, category);

        }
    }
}
