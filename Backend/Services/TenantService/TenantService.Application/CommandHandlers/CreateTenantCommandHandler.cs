using BuildingBlocks.EfCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Commands;
using TenantService.Core.Entities;
using TenantService.Infrastructure.Data;
using TenantService.Infrastructure.Repositories;

namespace TenantService.Application.CommandHandlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Tenant>
    {
        private readonly ITenantRepository<Tenant> _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTenantCommandHandler(ITenantRepository<Tenant> tenantRepository, IUnitOfWork unitOfWork)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tenant> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            await _tenantRepository.AddAsync(request.NewTenant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return request.NewTenant;
        }
    }
}
