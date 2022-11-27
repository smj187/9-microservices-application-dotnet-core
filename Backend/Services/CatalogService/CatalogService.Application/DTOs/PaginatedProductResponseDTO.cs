using BuildingBlocks.Domain;
using BuildingBlocks.Mongo.Helpers;
using CatalogService.Core.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CatalogService.Application.DTOs
{
    public class PaginatedProductResponseDTO
    {
        public PaginatedProductResponseDTO(List<Product> products, MongoPaginationResult pagination)
        {
            Products = products;
            Pagination = pagination;
        }

        public List<Product> Products { get; set; }
        public MongoPaginationResult Pagination { get; set; }
    }
}
