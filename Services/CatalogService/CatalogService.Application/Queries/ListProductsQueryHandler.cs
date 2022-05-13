using CatalogService.Core.Models;
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
    public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, IEnumerable<Product>>
    {
        private readonly ICatalogContext _context;

        public ListProductsQueryHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Find(p => true).ToListAsync(cancellationToken);
            return products;
        }
    }
}
