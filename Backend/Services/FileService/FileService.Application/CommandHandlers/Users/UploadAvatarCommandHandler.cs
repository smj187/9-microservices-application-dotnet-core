using BuildingBlocks.Domain.EfCore;
using FileService.Application.Commands.Users;
using FileService.Application.Services;
using FileService.Core.Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers.Users
{
    public class UploadAvatarCommandHandler : IRequestHandler<UploadAvatarCommand, Avatar>
    {
        private readonly ICloudService _cloudService;
        private readonly IAvatarRepository _avatarRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadAvatarCommandHandler(ICloudService cloudService, IAvatarRepository avatarRepository, IUnitOfWork unitOfWork)
        {
            _cloudService = cloudService;
            _avatarRepository = avatarRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Avatar> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
        {

            var url = await _cloudService.UploadUserAvatarAsync(request.FolderName, request.File, request.UserId);

            var avatar = new Avatar(request.UserId, url);

            await _avatarRepository.AddAsync(avatar);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return avatar;
        }
    }
}
