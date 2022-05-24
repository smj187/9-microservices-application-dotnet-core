using BuildingBlocks.Mongo;
using CatalogService.Application.Commands;
using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly IMongoRepository<Category> _mongoRepository;

        public CreateCategoryCommandHandler(IMongoRepository<Category> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.AddAsync(request.NewCategory);
        }
    }
}
