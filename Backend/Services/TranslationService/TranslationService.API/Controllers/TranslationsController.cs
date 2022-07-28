using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Application.Commands;
using TranslationService.Application.Queries;
using TranslationService.Contracts.v1.Contracts;
using TranslationService.Core.Aggregates;

namespace TranslationService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TranslationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TranslationsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> ListTranslationsAsync([FromQuery] string locale, [FromBody] List<string> request)
        {
            var query = new ListTranslationsQuery
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Keys = request
            };

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<CreateTranslationResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        [Route("{service}/{resource}/{identifier}")]
        public async Task<IActionResult> CreateTranslationAsync([FromRoute] string service, [FromRoute] string resource, [FromRoute] string identifier, [FromBody] List<CreateTranslationRequest> request)
        {
            var mapped = _mapper.Map<List<Translation>>(request, opts =>
            {
                opts.Items["tenant-id"] = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
                opts.Items["service"] = service;
                opts.Items["resource"] = resource;
                opts.Items["identifier"] = identifier;
            });

            var command = new CreateTranslationCommand
            {
                Translations = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<IReadOnlyCollection<CreateTranslationResponse>>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("multilingual/add")]
        public async Task<IActionResult> AddMultilingualAsync([FromBody] AddMultilingualRequest request)
        {
            var command = new AddMultilingualCommand
            {
                Key = request.Key,
                Locale = request.Locale,
                Value = request.Value
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CreateTranslationResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("multilingual/remove")]
        public async Task<IActionResult> RemoveMultilingualAsync([FromBody] RemoveMultilingualRequest request)
        {
            var command = new RemoveMultilingualCommand
            {
                Key = request.Key,
                Locale = request.Locale,
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CreateTranslationResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("multilingual/change")]
        public async Task<IActionResult> ChangeMultilingualAsync([FromBody] ChangeMultilingualRequest request)
        {
            var command = new PatchMultilingualCommand
            {
                Key = request.Key,
                Locale = request.Locale,
                Value = request.Value
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CreateTranslationResponse>(data);
            return Ok(result);
        }
    }
}
