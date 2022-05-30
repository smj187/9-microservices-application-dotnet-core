using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Categories
{
    public record CreateCategoryRequest([Required] string Name, string? Description, List<Guid>? ProductIds);
}
