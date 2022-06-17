using BuildingBlocks.Exceptions;
using CatalogService.Application.Queries.Sets;
using CatalogService.Core.Domain.Set;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Sets
{
    public class FindSetQueryHandler : IRequestHandler<FindSetQuery, Set>
    {
        private readonly ISetRepository _setRepository;

        public FindSetQueryHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<Set> Handle(FindSetQuery request, CancellationToken cancellationToken)
        {
            var set = await _setRepository.FindAsync(request.SetId);
            if (set == null)
            {
                throw new AggregateNotFoundException(nameof(Set), request.SetId);
            }

            return set;
        }
    }
}
