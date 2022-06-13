using BuildingBlocks.Exceptions;
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
    public class FindTenantQueryHandler : IRequestHandler<FindTenantQuery, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;

        public FindTenantQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> Handle(FindTenantQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.FindAsync(request.TenantId);
            if (tenant == null)
            {
                throw new AggregateNotFoundException(nameof(Tenant), request.TenantId);
            }

            return tenant;
        }
    }
}
