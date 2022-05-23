using CatalogService.Application.Commands;
using CatalogService.Application.Repositories.Products;
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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductCommandRepository _productCommandRepository;

        public CreateProductCommandHandler(IProductCommandRepository productCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productCommandRepository.CreateAsync(request.NewProduct);
        }
    }
}
