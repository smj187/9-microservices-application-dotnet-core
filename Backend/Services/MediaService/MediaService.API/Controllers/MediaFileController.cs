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
        public async Task<IActionResult> ListMediaFileAsync()
        {
            var query = new ListMediaFileQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IEnumerable<FileResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenantAsync([FromBody] CreateFileRequest createTenantRequest)
        {
            var mapped = _mapper.Map<Blob>(createTenantRequest);
            var command = new CreateMediaFileCommand
            {
                NewMediaFile = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<FileResponse>(data);
            return Ok(result);
        }
    }
}
