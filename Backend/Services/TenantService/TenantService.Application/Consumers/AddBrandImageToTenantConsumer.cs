using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileService.Contracts.v1.Events;
using TenantService.Core.Domain.Aggregates;
using BuildingBlocks.Exceptions;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Exceptions.Domain;
using TenantService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Multitenancy.Services;
using TenantService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace TenantService.Application.Consumers
{
    public class AddBrandImageToTenantConsumer : IConsumer<TenantBrandImageUploadResponseEvent>
    {
        private readonly IConfiguration _configuration;

        public AddBrandImageToTenantConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<TenantBrandImageUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var imageId = context.Message.ImageId;

            var optionsBuilder = new DbContextOptionsBuilder<TenantContext>();
            var tenantContext = new TenantContext(optionsBuilder.Options, _configuration, new MultitenancyService(tenantId, _configuration));
            var tenantRepository = new TenantRepository(tenantContext);

            var tenant = await tenantRepository.FindAsync(x => x.TenantId == tenantId);
            if (tenant == null)
            {
                throw new AggregateNotFoundException($"{tenantId} does not exist");
            }

            tenant.AddBrandImage(imageId);

            await tenantRepository.PatchAsync(tenant);
            await tenantContext.SaveChangesAsync();
        }
    }
}
