using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Products
{
    public record PatchVisibilityRequest([Required] bool IsVisible);
}
