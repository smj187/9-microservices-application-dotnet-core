using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Groups
{
    public record CreateGroupRequest([Required] string Name, string Description, [Required] decimal Price, string PriceDescription, List<string> Tags);
}
