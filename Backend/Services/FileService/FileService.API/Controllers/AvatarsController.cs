using AutoMapper;
using FileService.Application.Commands.Users;
using FileService.Application.Queries.Users;
using FileService.Contracts.v1;
using FileService.Contracts.v1.Contracts;
using FileService.Contracts.v1.Events;
using FileService.Core.Domain.User;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AvatarsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly string IMAGE_FOLDER_NAME = "user_avatars";

        public AvatarsController(IMapper mapper, IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [Route("{userid:guid}")]
        public async Task<IActionResult> FindAvatarAsync([FromRoute] Guid userId)
        {
            var query = new FindAvatarQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<UploadAvatarResponse>(data));
        }


        [HttpPost]
        public async Task<IActionResult> UploadAvatarAsync([FromForm] UploadAvatarRequest request)
        {
            var command = new UploadAvatarCommand
            {
                File = request.Image,
                FolderName = IMAGE_FOLDER_NAME,
                UserId = request.UserId,
            };

            var data = await _mediator.Send(command);

            await _publishEndpoint.Publish(new AvatarUploadSuccessEvent(request.UserId, data.Url));

            return Ok(_mapper.Map<UploadAvatarResponse>(data));
        }

    }
}
