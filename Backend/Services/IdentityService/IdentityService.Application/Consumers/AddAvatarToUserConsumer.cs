using BuildingBlocks.EfCore.Repositories.Interfaces;
using BuildingBlocks.Exceptions.Domain;
using FileService.Contracts.v1.Events;
using IdentityService.Application.Services;
using IdentityService.Core.Aggregates;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Consumers
{
    public class AddAvatarToUserConsumer : IConsumer<AvatarUploadResponseEvent>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAvatarToUserConsumer(IApplicationUserRepository applicationUserRepository, IUnitOfWork unitOfWork)
        {
            _applicationUserRepository = applicationUserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<AvatarUploadResponseEvent> context)
        {
            var avatar = context.Message.Url;

            var applicationUser = await _applicationUserRepository.FindAsync(context.Message.UserId);
            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), context.Message.UserId);
            }

            applicationUser.SetAvatar(context.Message.Url);

            await _applicationUserRepository.PatchAsync(applicationUser);
            await _unitOfWork.SaveChangesAsync(default);
        }
    }
}
