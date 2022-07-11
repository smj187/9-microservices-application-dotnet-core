using BuildingBlocks.Exceptions.Domain;
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
    public class GetTenantInformationQueryHandler : IRequestHandler<GetTenantInformationQuery, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;

        public GetTenantInformationQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> Handle(GetTenantInformationQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantRepository.ListAsync();
            if (!tenants.Any())
            {
                throw new AggregateNotFoundException($"this tenant does not have any information");
            }

            return tenants.FirstOrDefault();
        }
    }
}
