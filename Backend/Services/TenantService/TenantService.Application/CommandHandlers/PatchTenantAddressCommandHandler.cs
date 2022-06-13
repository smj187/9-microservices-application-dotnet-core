using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Commands;
using TenantService.Core.Domain.Aggregates;
using TenantService.Core.Domain.ValueObjects;

namespace TenantService.Application.CommandHandlers
{
    public class PatchTenantAddressCommandHandler : IRequestHandler<PatchTenantAddressCommand, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatchTenantAddressCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tenant> Handle(PatchTenantAddressCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.FindAsync(request.TenantId);
            if (tenant == null)
            {
                throw new AggregateNotFoundException(nameof(Tenant), request.TenantId);
            }

            tenant.PatchAddress(new Address(request.Street, request.City, request.State, request.Country, request.Zip));

            var patched = await _tenantRepository.PatchAsync(request.TenantId, tenant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return patched;
        }
    }
}
