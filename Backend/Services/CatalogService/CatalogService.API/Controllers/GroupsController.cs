using AutoMapper;
using CatalogService.Application.Commands;
using CatalogService.Application.Queries;
using CatalogService.Contracts.v1.Requests;
using CatalogService.Contracts.v1.Responses;
using CatalogService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GroupsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> ListGroupsAsync()
        {
            var query = new ListGroupsQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<GroupResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateGroupRequest createProductRequest)
        {
            var mapped = _mapper.Map<Group>(createProductRequest);
            var command = new CreateGroupCommand
            {
                NewGroup = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }
    }
}
