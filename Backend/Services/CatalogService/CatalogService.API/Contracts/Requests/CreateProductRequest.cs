using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Contracts.Requests
{
    public class CreateProductRequest
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Required]
        public string ImageUrl { get; set; } = default!;

        [Required]
        public decimal Price { get; set; }
    }
}
