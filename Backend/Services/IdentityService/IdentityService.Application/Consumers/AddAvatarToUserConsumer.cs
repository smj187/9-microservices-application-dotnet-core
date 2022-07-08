using BuildingBlocks.EfCore.Repositories;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Configurations;
using BuildingBlocks.Multitenancy.DependencyResolver;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using FileService.Contracts.v1.Events;
using IdentityService.Application.Services;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Infrastructure.Data;
using IdentityService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Consumers
{
    public class AddAvatarToUserConsumer : IConsumer<AvatarUploadResponseEvent>
    {
        private readonly IConfiguration _configuration;

        public AddAvatarToUserConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<AvatarUploadResponseEvent> context)
        {
            var tenantId = context.Message.TenantId;
            var avatar = context.Message.Url;

            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            var identityContext = new IdentityContext(optionsBuilder.Options, _configuration, new MultitenancyService(tenantId, _configuration));
            var repository = new ApplicationUserRepository(identityContext);

            var applicationUser = await repository.FindAsync(context.Message.UserId);
            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), context.Message.UserId);
            }

            applicationUser.SetAvatar(context.Message.Url);

            await repository.PatchAsync(applicationUser);
            await identityContext.SaveChangesAsync();
        }
    }
}
