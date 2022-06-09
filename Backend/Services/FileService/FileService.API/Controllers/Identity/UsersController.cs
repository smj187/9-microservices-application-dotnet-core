using AutoMapper;
using FileService.Application.Commands;
using FileService.Application.Queries;
using FileService.Contracts.v1.Contracts;
using FileService.Core.Domain;
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

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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

            return Ok(_mapper.Map<AvatarResponse>(data));
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
