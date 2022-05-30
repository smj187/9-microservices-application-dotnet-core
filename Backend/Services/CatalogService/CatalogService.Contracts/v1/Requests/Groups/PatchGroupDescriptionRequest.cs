using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Groups
{
    public class PatchGroupDescriptionRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? PriceDescription { get; set; }

        public List<string>? Tags { get; set; }
    }
}
