using CatalogService.Application.DTOs;
using CatalogService.Application.Queries.Sets;
using CatalogService.Core.Domain.Sets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Sets
{
    public class ListSetsQueryHandler : IRequestHandler<ListSetsQuery, PaginatedSetResponseDTO>
    {
        private readonly ISetRepository _setRepository;

        public ListSetsQueryHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<PaginatedSetResponseDTO> Handle(ListSetsQuery request, CancellationToken cancellationToken)
        {
            var result = await _setRepository.ListAsync(request.Page, request.PageSize);
             return new PaginatedSetResponseDTO(result.Item2.ToList(), result.mongoPaginationResult);
        }
    }
}
