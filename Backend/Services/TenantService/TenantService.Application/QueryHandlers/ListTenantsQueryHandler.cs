using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Queries;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.QueryHandlers
{
    public class ListTenantsQueryHandler : IRequestHandler<ListTenantsQuery, IReadOnlyCollection<Tenant>>
    {
        private readonly ITenantRepository _tenantRepository;

        public ListTenantsQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<IReadOnlyCollection<Tenant>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.ListAsync();
        }
    }
}
