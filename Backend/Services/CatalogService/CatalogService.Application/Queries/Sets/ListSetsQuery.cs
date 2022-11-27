using CatalogService.Application.DTOs;
using CatalogService.Core.Domain.Sets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Sets
{
    public class ListSetsQuery : IRequest<PaginatedSetResponseDTO>
    {
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
