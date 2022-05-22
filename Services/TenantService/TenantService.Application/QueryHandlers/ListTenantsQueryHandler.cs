using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Queries;
using TenantService.Application.Repositories;
using TenantService.Core.Entities;
using TenantService.Infrastructure.Data;

namespace TenantService.Application.QueryHandlers
{
    public class ListTenantsQueryHandler : IRequestHandler<ListTenantsQuery, IEnumerable<Tenant>>
    {
        private readonly ITenantRepository _tenantRepository;

        public ListTenantsQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<IEnumerable<Tenant>> Handle(ListTenantsQuery request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.ListTenantsAsync();
        }
    }
}
