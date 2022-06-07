using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;
using FileService.Contracts.v1.Events;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Consumers
{
    public class UpdateUserAvatarConsumer : IConsumer<AvatarUploadSuccessEvent>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserAvatarConsumer(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<AvatarUploadSuccessEvent> context)
        {
            var userId = context.Message.UserId;

            var user = await _userService.FindProfile(userId);
            if (user == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), userId);
            }

            user.SetAvatar(context.Message.Url);

            await _unitOfWork.SaveChangesAsync(default);

        }
    }
}
