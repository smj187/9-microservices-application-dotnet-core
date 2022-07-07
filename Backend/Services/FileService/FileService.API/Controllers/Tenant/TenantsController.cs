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

namespace FileService.API.Controllers.Tenant
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenantsController : ApiBaseController<TenantsController>
    {
        [HttpPost]
        [Route("upload-brand-image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantBrandImageAsync([FromForm, Required] UploadAvatarRequest request)
        {
            var data = await Mediator.Send(new UploadTenantImageCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Folder = "tenant_assets",
                AssetType = AssetType.TenantBrandImage,
                ExternalEntityId = request.ExternalEntityId,
                Image = request.Image
            });

            await PublishEndpoint.Publish(new TenantBrandImageUploadResponseEvent(data.TenantId, data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<TenantResponse>(data));
        }

        [HttpPost]
        [Route("upload-logo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantLogoImageAsync([FromForm, Required] UploadTenantLogoRequest request)
        {
            var data = await Mediator.Send(new UploadTenantImageCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Folder = "tenant_assets",
                AssetType = AssetType.TenantLogo,
                ExternalEntityId = request.ExternalEntityId,
                Image = request.Image
            });

            await PublishEndpoint.Publish(new TenantLogoUploadResponseEvent(data.TenantId, data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<TenantResponse>(data));
        }

        [HttpPost]
        [Route("upload-video")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantVideoAsync([FromForm, Required] UploadTenantVideoRequest request)
        {
            var data = await Mediator.Send(new UploadTenantVideoCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Folder = "tenant_assets",
                AssetType = AssetType.TenantVideo,
                ExternalEntityId = request.ExternalEntityId,
                Video = request.Video
            });

            await PublishEndpoint.Publish(new TenantVideoUploadResponseEvent(data.TenantId, data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<TenantResponse>(data));
        }

        [HttpPost]
        [Route("upload-banner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantBannerImageAsync([FromForm, Required] UploadTenantBannerRequest request)
        {
            var data = await Mediator.Send(new UploadTenantImageCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Folder = "tenant_assets",
                AssetType = AssetType.TenantBanner,
                ExternalEntityId = request.ExternalEntityId,
                Image = request.Image
            });

            await PublishEndpoint.Publish(new TenantBannerUploadResponseEvent(data.TenantId, data.ExternalEntityId, data.Id));
            return Ok(Mapper.Map<TenantResponse>(data));
        }

        [HttpGet]
        [Route("{externalentityId:guid}/find-asset")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindTenantAssetAsync([FromRoute, Required] Guid externalEntityId)
        {
            var data = await Mediator.Send(new FindExternalEntityAssetQuery
            {
                ExternalEntityId = externalEntityId
            });
            return Ok(Mapper.Map<TenantResponse>(data));
        }

        [HttpGet]
        [Route("{externalentityId:guid}/list-assets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<TenantResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListTenantAssetsAsync([FromRoute, Required] Guid externalEntityId)
        {
            var data = await Mediator.Send(new ListAssetsQuery
            {
                ExternalEntityId = externalEntityId
            });
            return Ok(Mapper.Map<IReadOnlyCollection<TenantResponse>>(data));
        }
    }
}
