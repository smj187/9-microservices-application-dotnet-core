using CatalogService.Application.Commands;
using CatalogService.Application.Repositories.Categories;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Repositories;
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
        private readonly ICategoryCommandRepository _categoryCommandRepository;

        public CreateCategoryCommandHandler(ICategoryCommandRepository categoryCommandRepository)
        {
            _categoryCommandRepository = categoryCommandRepository;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryCommandRepository.CreateAsync(request.NewCategory);
        }
    }
}
