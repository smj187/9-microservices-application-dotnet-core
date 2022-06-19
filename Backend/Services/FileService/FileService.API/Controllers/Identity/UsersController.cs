using BuildingBlocks.Extensions.Controllers;
using FileService.Application.Commands;
using FileService.Application.Queries;
using FileService.Contracts.v1.Contracts;
using FileService.Contracts.v1.Events;
using FileService.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Controllers.Identity
{
    [Route("api/v1/[controller]")]
    public class UsersController : ApiBaseController<UsersController>
    {
        [HttpPost]
        [Route("upload-avatar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadAvatarImageAsync([FromForm, Required] UploadAvatarRequest request)
        {
            var data = await Mediator.Send(new UploadAvatarCommand
            {
                Folder = "avatar_assets",
                AssetType = AssetType.IdentityAvatarImage,
                UserId = request.ExternalEntityId,
                Image = request.Image
            });


            await PublishEndpoint.Publish(new AvatarUploadResponseEvent(data.ExternalEntityId, data.Url));
            return Ok(Mapper.Map<AvatarResponse>(data));
        }

        [HttpGet]
        [Route("{externalentityId:guid}/find-avatar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindAvatarAssetAsync([FromRoute, Required] Guid externalEntityId)
        {
            var data = await Mediator.Send(new FindAvatarQuery
            {
                ExternalEntityId = externalEntityId
            });
            return Ok(Mapper.Map<AvatarResponse>(data));
        }
    }
}
