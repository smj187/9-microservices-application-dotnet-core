using AutoMapper;
using CatalogService.Application.Commands;
using CatalogService.Application.Commands.Groups;
using CatalogService.Application.Queries;
using CatalogService.Application.Queries.Groups;
using CatalogService.Contracts.v1.Contracts;
using CatalogService.Core.Domain.Group;
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
        public async Task<IActionResult> CreateGroupAsync([FromBody] CreateGroupRequest request)
        {
            var mapped = _mapper.Map<Group>(request);
            var command = new CreateGroupCommand
            {
                NewGroup = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }


        [HttpGet]
        [Route("{groupId:guid}/find")]
        public async Task<IActionResult> FindGroupAsync([FromRoute] Guid groupId)
        {
            var query = new FindGroupQuery
            {
                GroupId = groupId
            };

            var data = await _mediator.Send(query);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }




        [HttpPatch]
        [Route("{groupId:guid}/price")]
        public async Task<IActionResult> ChangeGroupPriceAsync([FromRoute] Guid groupId, [FromBody] PatchGroupPriceRequest request)
        {
            var command = new PatchGroupPriceCommand
            {
                GroupId = groupId,
                Price = request.Price
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{groupId:guid}/description")]
        public async Task<IActionResult> ChangeGroupDescriptionAsync([FromRoute] Guid groupId, [FromBody] PatchGroupDescriptionRequest request)
        {
            var command = new PatchGroupDescriptionCommand
            {
                GroupId = groupId,
                Name = request.Name,
                Description = request.Description,
                PriceDescription = request.PriceDescription,
                Tags = request.Tags
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{groupId:guid}/visibility")]
        public async Task<IActionResult> ChangeGroupVisibilityAsync([FromRoute] Guid groupId, [FromBody] PatchGroupVisibilityRequest request)
        {
            var command = new PatchGroupVisibilityCommand
            {
                GroupId = groupId,
                IsVisible = request.IsVisible
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }


        [HttpPatch]
        [Route("{groupid:guid}/add-product/{productid:guid}")]
        public async Task<IActionResult> AddGroupToCategoryAsync([FromRoute] Guid groupId, [FromRoute] Guid productId)
        {
            var command = new AddProductToGroupCommand
            {
                GroupId = groupId,
                ProductId = productId
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{groupid:guid}/remove-product/{productid:guid}")]
        public async Task<IActionResult> RemoveGroupFromCategoryAsync([FromRoute] Guid groupId, [FromRoute] Guid productId)
        {
            var command = new RemoveProductFromGroupCommand
            {
                GroupId = groupId,
                ProductId = productId
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<GroupResponse>(data);
            return Ok(result);
        }
    }
}
