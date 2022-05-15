using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Data;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries
{
    public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICatalogContext _context;

        public ListCategoriesQueryHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.Find(c => true).ToListAsync(cancellationToken);
            return categories;
        }
    }
}
