using AutoMapper;
using FileService.Application.Commands.Images;
using FileService.Application.Queries.Images;
using FileService.Contracts.v1;
using FileService.Core.Domain.Image;
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
    public class ImagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string IMAGE_FOLDER_NAME = "test_images";

        public ImagesController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListImagesAsync()
        {
            var query = new ListImagesQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<ImageResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync([FromForm] UploadImageRequest request)
        {
            var mapped = _mapper.Map<ImageFile>(request);

            var command = new UploadImageCommand
            {
                FolderName = IMAGE_FOLDER_NAME,
                NewImageFile = mapped,
                File = request.Image
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ImageResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        [Route("{imageid:guid}/find")]
        public async Task<IActionResult> FindImageAsync([FromRoute] Guid imageId)
        {
            var query = new FindImageQuery
            {
                ImageId = imageId
            };

            var data = await _mediator.Send(query);

            var result = _mapper.Map<ImageResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{imageid:guid}/description")]
        public async Task<IActionResult> PatchImageDescriptionAsync([FromRoute] Guid imageId, [FromBody] PatchImageDescriptionRequest request)
        {
            var command = new PatchImageDescriptionCommand
            {
                ImageId = imageId,
                Title = request.Title,
                Description = request.Description,
                Tags = request.Tags
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ImageResponse>(data);
            return Ok(result);
        }
    }
}
