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

namespace FileService.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("upload-image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadCategoryImageAsync([FromForm, Required] UploadCategoryImageRequest request)
        {
            var command = new UploadImageCommand
            {
                Folder = "category_images",
                Description = request.Description,
                Title = request.Title,
                Tags = request.Tags,
                ExternalEntityId = request.ExternalEntityId,
                Image = request.Image,
                AssetType = AssetType.CatalogCategoryImage,
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AssetResponse>(data));
        }


        [HttpPost]
        [Route("upload-video")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadCategoryVideoAsync([FromForm, Required] UploadCategoryVideoRequest request)
        {
            var command = new UploadVideoCommand
            {
                Folder = "category_videos",
                Description = request.Description,
                Title = request.Title,
                Tags = request.Tags,
                ExternalEntityId = request.ExternalEntityId,
                Video = request.Video,
                AssetType = AssetType.CatalogCategoryVideo,
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AssetResponse>(data));
        }


        [HttpGet]
        [Route("{externalentityid:guid}/list-assets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<AssetResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListCategoryAssetsAsync([FromRoute, Required] Guid externalEntityId)
        {
            var query = new ListAssetsQuery
            {
                ExternalEntityId = externalEntityId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<IReadOnlyCollection<AssetResponse>>(data));
        }


        [HttpGet]
        [Route("{assetid:guid}/find-asset")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindCategoryAssetAsync([FromRoute, Required] Guid assetId)
        {
            var query = new FindAssetQuery
            {
                AssetId = assetId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<AssetResponse>(data));
        }


        [HttpPatch]
        [Route("{assetid:guid}/change-description")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchCategoryDescriptionAsync([FromRoute, Required] Guid assetId, [FromBody, Required] PatchCategoryImageDescriptionRequest request)
        {
            var command = new PatchAssetDescriptionCommand
            {
                AssetId = assetId,
                Title = request.Title,
                Description = request.Description,
                Tags = request.Tags
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<AssetResponse>(data));
        }
    }
}
