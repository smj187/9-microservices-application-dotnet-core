using AutoMapper;
using FileService.Application.Commands.Videos;
using FileService.Application.Queries.Videos;
using FileService.Contracts.v1;
using FileService.Core.Domain.Video;
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
    public class VideosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VideosController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListVideosAsync()
        {
            var query = new ListVideosQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<VideoResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideoAsync([FromForm] UploadVideoRequest request)
        {
            var mapped = _mapper.Map<VideoFile>(request);

            var command = new UploadVideoCommand
            {
                FolderName = "test_videos",
                NewVideoFile = mapped,
                File = request.Video
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<VideoResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        [Route("{videoid:guid}/find")]
        public async Task<IActionResult> FindVideoAsync([FromRoute] Guid videoId)
        {
            var query = new FindVideoQuery
            {
                VideoId = videoId
            };

            var data = await _mediator.Send(query);

            var result = _mapper.Map<VideoResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{videoid:guid}/description")]
        public async Task<IActionResult> PatchVideoDescriptionAsync([FromRoute] Guid videoId, [FromBody] PatchVideoDescriptionRequest request)
        {
            var command = new PatchVideoDescriptionCommand
            {
                VideoId = videoId,
                Title = request.Title,
                Description = request.Description,
                Tags = request.Tags
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<VideoResponse>(data);
            return Ok(result);
        }
    }
}
