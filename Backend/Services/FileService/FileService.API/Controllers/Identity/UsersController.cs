using AutoMapper;
using FileService.Application.Commands;
using FileService.Application.Queries;
using FileService.Contracts.v1.Contracts;
using FileService.Contracts.v1.Events;
using FileService.Core.Domain;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Controllers.Identity
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UsersController(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        [Route("upload-avatar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadAvatarImageAsync([FromForm, Required] UploadAvatarRequest request)
        {
            var command = new UploadAvatarCommand
            {
                Folder = "avatar_assets",
                AssetType = AssetType.IdentityAvatarImage,
                UserId = request.ExternalEntityId,
                Image = request.Image
            };

            var data = await _mediator.Send(command);


            await _publishEndpoint.Publish(new AvatarUploadResponseEvent(data.ExternalEntityId, data.Url));
            var result = _mapper.Map<AvatarResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        [Route("{externalentityId:guid}/find-avatar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindAvatarAssetAsync([FromRoute, Required] Guid externalEntityId)
        {
            var query = new FindAvatarQuery
            {
                ExternalEntityId = externalEntityId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<AvatarResponse>(data));
        }
    }
}
