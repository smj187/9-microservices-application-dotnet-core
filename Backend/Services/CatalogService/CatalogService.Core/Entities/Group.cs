using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Entities
{
    public class Group : AggregateRoot
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;
        public decimal Price { get; set; }


        private readonly List<Product> _products = new();
        public List<Product> Products => _products;


        public void AddToGroup(Product product)
        {
            Guard.Against.Null(product, nameof(product));
            _products.Add(product);
        }

        public void RemoveFromGroup(Guid productId)
        {
            Guard.Against.NullOrEmpty(productId, nameof(productId));

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if(product == null)
            {
                throw new AggregateNotFoundException(nameof(Product), productId);
            }

            _products.Remove(product);
        }
    }
}
