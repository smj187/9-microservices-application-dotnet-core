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
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Controllers.Catalog
{
    [Route("api/v1/[controller]")]
    public class ProductsController : ApiBaseController<ProductsController>
    {
        [HttpPost]
        [Route("upload-image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadProductImageAsync([FromForm, Required] UploadProductImageRequest request)
        {
            var data = await Mediator.Send(new UploadImageCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Folder = "product_images",
                Description = request.Description,
                Title = request.Title,
                Tags = request.Tags,
                ExternalEntityId = request.ExternalEntityId,
                Image = request.Image,
                AssetType = AssetType.CatalogProductImage,
            });

            await PublishEndpoint.Publish(new ProductImageUploadResponseEvent(data.TenantId, data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<AssetResponse>(data));
        }


        [HttpPost]
        [Route("upload-video")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadProductVideoAsync([FromForm, Required] UploadProductVideoRequest request)
        {
            var data = await Mediator.Send(new UploadVideoCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Folder = "product_videos",
                Description = request.Description,
                Title = request.Title,
                Tags = request.Tags,
                ExternalEntityId = request.ExternalEntityId,
                Video = request.Video,
                AssetType = AssetType.CatalogProductVideo,
            });

            await PublishEndpoint.Publish(new ProductVideoUploadResponseEvent(data.TenantId, data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<AssetResponse>(data));
        }


        [HttpGet]
        [Route("{externalentityid:guid}/list-assets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<AssetResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListProductAssetsAsync([FromRoute, Required] Guid externalEntityId)
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
        public async Task<IActionResult> FindProductAssetAsync([FromRoute, Required] Guid assetId)
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
        public async Task<IActionResult> PatchProductDescriptionAsync([FromRoute, Required] Guid assetId, [FromBody, Required] PatchProductImageDescriptionRequest request)
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
