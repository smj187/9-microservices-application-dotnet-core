using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Products
{
    public class Nutrition : ValueObject
    {
        public Nutrition(string name, int weight)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.Null(weight, nameof(weight));
            Name = name;
            Weight = weight;
        }

        public string Name { get; init; }
        public int Weight { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Weight;
        }
    }
}
