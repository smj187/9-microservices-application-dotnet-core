using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Commands;
using TenantService.Application.Repositories;
using TenantService.Core.Entities;
using TenantService.Infrastructure.Data;

namespace TenantService.Application.CommandHandlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;

        public CreateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Tenant> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.CreateTenantAsync(request.NewTenant);
        }
    }
}
