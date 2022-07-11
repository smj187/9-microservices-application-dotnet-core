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
    public class AddLogoToTenantConsumer : IConsumer<TenantLogoUploadResponseEvent>
    {
        private readonly IConfiguration _configuration;

        public AddLogoToTenantConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<TenantLogoUploadResponseEvent> context)
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

            tenant.AddLogo(imageId);

            await tenantRepository.PatchAsync(tenant);
            await tenantContext.SaveChangesAsync();
        }
    }
}
