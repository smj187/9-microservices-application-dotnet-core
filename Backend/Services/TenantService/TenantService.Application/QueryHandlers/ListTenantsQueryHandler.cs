using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Queries;
using TenantService.Core.Entities;
using TenantService.Infrastructure.Repositories;

namespace TenantService.Application.QueryHandlers
{
    public class ListTenantsQueryHandler : IRequestHandler<ListTenantsQuery, IEnumerable<Tenant>>
    {
        private readonly ITenantRepository<Tenant> _tenantRepository;

        public ListTenantsQueryHandler(ITenantRepository<Tenant> tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<IEnumerable<Tenant>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.ListAsync();
        }
    }
}
