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
using TenantService.Core.Domain.Enumerations;
using TenantService.Core.Domain.ValueObjects;

namespace TenantService.Application.CommandHandlers
{
    public class AddWorkingdayToTenantCommandHandler : IRequestHandler<AddWorkingdayToTenantCommand, Tenant>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddWorkingdayToTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tenant> Handle(AddWorkingdayToTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.FindAsync(request.TenantId);
            if (tenant == null)
            {
                throw new AggregateNotFoundException(nameof(Tenant), request.TenantId);
            }

            var day = new Workingday(Weekday.Create(request.Workingday), request.OpeningHour, request.ClosingHour, request.OpeningMinute, request.ClosingMinute);
            tenant.AddWorkingday(day);

            var patched = await _tenantRepository.PatchAsync(request.TenantId, tenant);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return patched;
        }
    }
}
