using BuildingBlocks.Exceptions;
using FileService.Application.Queries.Users;
using FileService.Core.Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.QueryHandlers.Users
{
    public class FindAvatarQueryHandler : IRequestHandler<FindAvatarQuery, Avatar>
    {
        private readonly IAvatarRepository _avatarRepository;

        public FindAvatarQueryHandler(IAvatarRepository avatarRepository)
        {
            _avatarRepository = avatarRepository;
        }

        public async Task<Avatar> Handle(FindAvatarQuery request, CancellationToken cancellationToken)
        {
            var avatar = await _avatarRepository.FindAsync(x => x.UserId == request.UserId);

            if (avatar == null)
            {
                throw new AggregateNotFoundException(nameof(Avatar), request.UserId);
            }

            return avatar;
        }
    }
}
