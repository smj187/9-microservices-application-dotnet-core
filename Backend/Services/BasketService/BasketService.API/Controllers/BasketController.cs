using AutoMapper;
using BasketService.Application.Commands;
using BasketService.Application.Queries;
using BasketService.Contracts.v1.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BasketController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<BasketResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListBasketsAsync()
        {
            var query = new ListBasketsQuery();

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<IReadOnlyCollection<BasketResponse>>(data));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBasketAsync()
        {
            var command = new CreateBasketCommand();

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<BasketResponse>(data));
        }


        [HttpGet]
        [Route("{basketid:guid}/find-basket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindBasketAsync([FromRoute, Required] Guid basketId)
        {
            var query = new FindBasketQuery
            {
                BasketId = basketId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/add-item")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddItemToBasketAsync([FromRoute, Required] Guid basketId, [FromBody, Required] AddItemToBasketRequest request)
        {
            var command = new AddItemToBasketCommand
            {
                BasketId = basketId,
                ItemId = request.ItemId,
                ItemName = request.ItemName,
                ItemImage = request.ItemImage,
                Price = request.Price,
                Quantity = request.Quantity
            };


            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/remove-item")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveItemFromBasketAsync([FromRoute, Required] Guid basketId, [FromBody, Required] RemoveItemFromBasketRequest request)
        {
            var command = new RemoveItemFromBasketCommand
            {
                BasketId = basketId,
                ItemId = request.ItemId
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/clear")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ClearBasketAsync([FromRoute, Required] Guid basketId)
        {
            var command = new ClearBasketCommand
            {
                BasketId = basketId
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/assign-user/{userid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignBasketToUserAsync([FromRoute, Required] Guid basketId, [FromRoute, Required] Guid userId)
        {
            var command = new AssignUserToBasketCommand
            {
                BasketId = basketId,
                UserId = userId
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<BasketResponse>(data));
        }
    }
}
