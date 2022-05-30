using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Responses
{
    public class CategoryDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CategoryProductResponse> Products { get; set; }
        public List<CategoryImagesResponse> Images { get; set; }
    }
    //public record CategoryDetailsResponse(Guid Id, string Name, string Description, List<CategoryProductResponse> Products, List<CategoryImagesResponse> Images);
    public record CategoryImagesResponse(string Url, string Name, string Description, string Tags);
    public record CategoryProductResponse(Guid Id, string Name, string Description, decimal Price, List<CategoryProductImagesResponse> Images);
    public record CategoryProductImagesResponse(string Url, string Name, string Description, string Tags);
}
