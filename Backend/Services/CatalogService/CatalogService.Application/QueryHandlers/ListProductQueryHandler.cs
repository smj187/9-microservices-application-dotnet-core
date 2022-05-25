using BuildingBlocks.Mongo;
using CatalogService.Application.Queries;
using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers
{
    public class ListProductQueryHandler : IRequestHandler<ListProductsQuery, IEnumerable<Product>>
    {
        private readonly IMongoRepository<Product> _mongoRepository;

        public ListProductQueryHandler(IMongoRepository<Product> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.ListAsync();
        }
    }
}
