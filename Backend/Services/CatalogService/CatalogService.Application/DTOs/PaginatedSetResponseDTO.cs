using BuildingBlocks.Mongo.Helpers;
using CatalogService.Core.Domain.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.DTOs
{
    public class PaginatedSetResponseDTO
    {
        public List<Set> Sets { get; set; }
        public MongoPaginationResult Pagination { get; set; }

        public PaginatedSetResponseDTO(List<Set> sets, MongoPaginationResult pagination)
        {
            Sets = sets;
            Pagination = pagination;
        }
    }
}
