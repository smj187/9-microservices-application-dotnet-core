using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using FileService.Contracts.v1.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TenantService.Core.Domain.Aggregates;
using TenantService.Infrastructure.Data;
using TenantService.Infrastructure.Repositories;

namespace TenantService.Application.Consumers
{
    public class AddVideoToTenantConsumer : IConsumer<TenantVideoUploadResponseEvent>
    {
        private readonly IConfiguration _configuration;

        public AddVideoToTenantConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<TenantVideoUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var videoId = context.Message.VideoId;

            var optionsBuilder = new DbContextOptionsBuilder<TenantContext>();
            var tenantContext = new TenantContext(optionsBuilder.Options, _configuration, new MultitenancyService(tenantId, _configuration));
            var tenantRepository = new TenantRepository(tenantContext);

            var tenant = await tenantRepository.FindAsync(x => x.TenantId == tenantId);
            if (tenant == null)
            {
                throw new AggregateNotFoundException($"{tenantId} does not exist");
            }

            tenant.AddVideo(videoId);

            await tenantRepository.PatchAsync(tenant);
            await tenantContext.SaveChangesAsync();
        }
    }
}
