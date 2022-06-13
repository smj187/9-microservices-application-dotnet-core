using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileService.Contracts.v1.Events;
using TenantService.Core.Domain.Aggregates;
using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;

namespace TenantService.Application.Consumers
{
    public class AddBrandImageToTenantConsumer : IConsumer<TenantBrandImageUploadResponseEvent>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddBrandImageToTenantConsumer(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TenantBrandImageUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var imageId = context.Message.ImageId;

            var tenant = await _tenantRepository.FindAsync(tenantId);
            if (tenant == null)
            {
                throw new AggregateNotFoundException(nameof(Tenant), tenantId);
            }

            tenant.AddBrandImage(imageId);

            await _tenantRepository.PatchAsync(tenantId, tenant);
            await _unitOfWork.SaveChangesAsync(default);
        }
    }
}
