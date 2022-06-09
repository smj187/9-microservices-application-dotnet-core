using FileService.Contracts.v1.Events;
using IdentityService.Application.Services;
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
        private readonly IUserService _userService;

        public AddAvatarToUserConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<AvatarUploadResponseEvent> context)
        {
            var userId = context.Message.UserId;
            var avatar = context.Message.Url;

            await _userService.AddAvatarToProfileAsync(userId, avatar);
        }
    }
}
