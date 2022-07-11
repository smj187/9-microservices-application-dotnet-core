using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Exceptions.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Commands;
using TenantService.Core.Domain.Aggregates;
using TenantService.Core.Domain.ValueObjects;
using TenantService.Infrastructure.Data;

namespace TenantService.Application.CommandHandlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tenant> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var existingTenant = await _tenantRepository.ExistsAsync(expression => expression.TenantId == request.TenantId);
            if (existingTenant)
            {
                throw new DuplicateAggregateException($"this client already has a tenant");
            }

            var address = new Address(request.State, request.City, request.State, request.Country, request.Zip);
            var tenant = new Tenant(request.TenantId, request.Name, address, request.Email, request.Phone);

            await _tenantRepository.AddAsync(tenant);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tenant;
        }
    }
}
