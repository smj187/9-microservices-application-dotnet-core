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

namespace FileService.API.Controllers.Tenant
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public TenantsController(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        [Route("upload-brand-image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantBrandImageAsync([FromForm, Required] UploadAvatarRequest request)
        {
            var command = new UploadTenantImageCommand
            {
                Folder = "tenant_assets",
                AssetType = AssetType.TenantBrandImage,
                TenantId = request.ExternalEntityId,
                Image = request.Image
            };

            var data = await _mediator.Send(command);


            await _publishEndpoint.Publish(new TenantBrandImageUploadResponseEvent(data.ExternalEntityId, data.Id));
            var result = _mapper.Map<TenantResponse>(data);
            return Ok(result);
        }

        [HttpPost]
        [Route("upload-logo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantLogoImageAsync([FromForm, Required] UploadTenantLogoRequest request)
        {
            var command = new UploadTenantImageCommand
            {
                Folder = "tenant_assets",
                AssetType = AssetType.TenantLogo,
                TenantId = request.ExternalEntityId,
                Image = request.Image
            };

            var data = await _mediator.Send(command);


            await _publishEndpoint.Publish(new TenantLogoUploadResponseEvent(data.ExternalEntityId, data.Id));
            var result = _mapper.Map<TenantResponse>(data);
            return Ok(result);
        }

        [HttpPost]
        [Route("upload-video")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantVideoAsync([FromForm, Required] UploadTenantVideoRequest request)
        {
            var command = new UploadTenantVideoCommand
            {
                Folder = "tenant_assets",
                AssetType = AssetType.TenantVideo,
                TenantId = request.ExternalEntityId,
                Video = request.Video
            };

            var data = await _mediator.Send(command);


            await _publishEndpoint.Publish(new TenantVideoUploadResponseEvent(data.ExternalEntityId, data.Id));
            var result = _mapper.Map<TenantResponse>(data);
            return Ok(result);
        }

        [HttpPost]
        [Route("upload-banner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadTenantBannerImageAsync([FromForm, Required] UploadTenantBannerRequest request)
        {
            var command = new UploadTenantImageCommand
            {
                Folder = "tenant_assets",
                AssetType = AssetType.TenantBanner,
                TenantId = request.ExternalEntityId,
                Image = request.Image
            };

            var data = await _mediator.Send(command);


            await _publishEndpoint.Publish(new TenantBannerUploadResponseEvent(data.ExternalEntityId, data.Id));
            var result = _mapper.Map<TenantResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        [Route("{externalentityId:guid}/find-asset")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindTenantAssetAsync([FromRoute, Required] Guid externalEntityId)
        {
            var query = new FindExternalEntityAssetQuery
            {
                ExternalEntityId = externalEntityId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<TenantResponse>(data));
        }

        [HttpGet]
        [Route("{externalentityId:guid}/list-assets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<TenantResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListTenantAssetsAsync([FromRoute, Required] Guid externalEntityId)
        {
            var query = new ListAssetsQuery
            {
                ExternalEntityId = externalEntityId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<IReadOnlyCollection<TenantResponse>>(data));
        }
    }
}
