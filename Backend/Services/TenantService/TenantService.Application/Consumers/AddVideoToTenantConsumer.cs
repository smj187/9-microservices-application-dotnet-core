using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;
using FileService.Contracts.v1.Events;
using MassTransit;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Application.Consumers
{
    public class AddVideoToTenantConsumer : IConsumer<TenantVideoUploadResponseEvent>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddVideoToTenantConsumer(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
        {
            _tenantRepository = tenantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<TenantVideoUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var videoId = context.Message.VideoId;

            var tenant = await _tenantRepository.FindAsync(tenantId);
            if (tenant == null)
            {
                throw new AggregateNotFoundException(nameof(Tenant), tenantId);
            }

            tenant.AddVideo(videoId);

            await _tenantRepository.PatchAsync(tenantId, tenant);
            await _unitOfWork.SaveChangesAsync(default);
        }
    }
}
