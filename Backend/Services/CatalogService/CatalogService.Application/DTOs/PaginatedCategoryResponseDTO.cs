using BuildingBlocks.Mongo.Helpers;
using CatalogService.Core.Domain.Categories;
using CatalogService.Core.Domain.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.DTOs
{
    public class PaginatedCategoryResponseDTO
    {
        public List<Category> Categories { get; set; }
        public MongoPaginationResult Pagination { get; set; }

        public PaginatedCategoryResponseDTO(List<Category> categories, MongoPaginationResult pagination)
        {
            Categories = categories;
            Pagination = pagination;
        }
    }
}
