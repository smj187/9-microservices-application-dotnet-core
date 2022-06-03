using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Product
{
    public class Allergen : ValueObject
    {
        public Allergen(string abbr, string name)
        {
            Guard.Against.NullOrWhiteSpace(abbr, nameof(abbr));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Abbr = abbr;
            Name = name;
        }

        public string Abbr { get; init; }
        public string Name { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Abbr;
            yield return Name;
        }
    }
}
