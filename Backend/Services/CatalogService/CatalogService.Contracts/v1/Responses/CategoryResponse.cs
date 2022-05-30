using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Responses
{
    public class CategorySummaryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public List<CategoryImagesResponse> Images { get; set; } = default!;
    }
}
