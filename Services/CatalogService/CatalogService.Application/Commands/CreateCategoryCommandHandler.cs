using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly ICatalogContext _context;

        public CreateCategoryCommandHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _context.Categories.InsertOneAsync(request.NewCategory);
            return request.NewCategory;
        }
    }
}
