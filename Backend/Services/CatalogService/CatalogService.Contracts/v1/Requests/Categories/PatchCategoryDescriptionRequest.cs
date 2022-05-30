using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Categories
{
    public record PatchCategoryDescriptionRequest(string Name, string Description);
}
