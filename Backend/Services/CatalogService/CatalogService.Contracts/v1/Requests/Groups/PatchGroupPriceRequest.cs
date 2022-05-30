using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Groups
{
    public class PatchGroupPriceRequest
    {
        public decimal Price { get; set; }
    }
}
