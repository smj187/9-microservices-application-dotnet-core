using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Contracts.Reponses
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;
    }
}
