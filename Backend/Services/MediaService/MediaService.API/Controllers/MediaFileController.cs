using AutoMapper;
using MediaService.Application.Commands;
using MediaService.Application.Queries;
using MediaService.Contracts.v1.Requests;
using MediaService.Contracts.v1.Responses;
using MediaService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MediaFileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MediaFileController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListMediaFileAsync()
        {
            var query = new ListMediasQuery
            {
                FolderName = "my_test_folder"
            };

            var data = await _mediator.Send(query);

            //var result = _mapper.Map<IReadOnlyCollection<FileResponse>>(data);
            return Ok(data);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFileAsync([FromForm] IFormFile file, [FromForm] string title, [FromForm] string description, [FromForm] string tags)
        {
            var command = new UploadMediaCommand
            {
                File = file,
                Title = title,
                Description = description,
                Tags = tags,
                FolderName = "my_test_folder"
            };

            var data = await _mediator.Send(command);


            return Ok(data);
        }

    }
}
