using BuildingBlocks.Controllers;
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

namespace FileService.API.Controllers.Catalog
{
    [Route("api/v1/[controller]")]
    public class SetsController : ApiBaseController<SetsController>
    {
        [HttpPost]
        [Route("upload-image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadSetImageAsync([FromForm, Required] UploadSetImageRequest request)
        {
            var data = await Mediator.Send(new UploadImageCommand
            {
                Folder = "set_images",
                Description = request.Description,
                Title = request.Title,
                Tags = request.Tags,
                ExternalEntityId = request.ExternalEntityId,
                Image = request.Image,
                AssetType = AssetType.CatalogSetImage,
            });

            await PublishEndpoint.Publish(new SetImageUploadResponseEvent(data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<AssetResponse>(data));
        }


        [HttpPost]
        [Route("upload-video")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadSetVideoAsync([FromForm, Required] UploadSetVideoRequest request)
        {
            var data = await Mediator.Send(new UploadVideoCommand
            {
                Folder = "set_videos",
                Description = request.Description,
                Title = request.Title,
                Tags = request.Tags,
                ExternalEntityId = request.ExternalEntityId,
                Video = request.Video,
                AssetType = AssetType.CatalogSetVideo,
            });

            await PublishEndpoint.Publish(new SetVideoUploadResponseEvent(data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<AssetResponse>(data));
        }


        [HttpGet]
        [Route("{externalentityid:guid}/list-assets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<AssetResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListSetAssetsAsync([FromRoute, Required] Guid externalEntityId)
        {
            var data = await Mediator.Send(new ListAssetsQuery
            {
                ExternalEntityId = externalEntityId
            });

            return Ok(Mapper.Map<IReadOnlyCollection<AssetResponse>>(data));
        }


        [HttpGet]
        [Route("{assetid:guid}/find-asset")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindSetAssetAsync([FromRoute, Required] Guid assetId)
        {
            var data = await Mediator.Send(new FindAssetQuery
            {
                AssetId = assetId
            });

            return Ok(Mapper.Map<AssetResponse>(data));
        }


        [HttpPatch]
        [Route("{assetid:guid}/change-description")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchSetDescriptionAsync([FromRoute, Required] Guid assetId, [FromBody, Required] PatchSetImageDescriptionRequest request)
        {
            var data = await Mediator.Send(new PatchAssetDescriptionCommand
            {
                AssetId = assetId,
                Title = request.Title,
                Description = request.Description,
                Tags = request.Tags
            });
            return Ok(Mapper.Map<AssetResponse>(data));
        }
    }
}
