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
    public class ListSetsQueryHandler : IRequestHandler<ListSetsQuery, IReadOnlyCollection<Set>>
    {
        private readonly ISetRepository _setRepository;

        public ListSetsQueryHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<IReadOnlyCollection<Set>> Handle(ListSetsQuery request, CancellationToken cancellationToken)
        {
            return await _setRepository.ListAsync();
        }
    }
}
