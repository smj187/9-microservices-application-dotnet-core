using AutoMapper;
using BasketService.Application.Commands;
using BasketService.Application.Queries;
using BasketService.Contracts.v1.Commands;
using BasketService.Contracts.v1.Contracts;
using BasketService.Core.Domain;
using BuildingBlocks.Extensions.Controllers;
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
    [Route("api/v1/[controller]")]
    public class BasketController : ApiBaseController<BasketController>
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<BasketResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListBasketsAsync()
        {
            var query = new ListBasketsQuery
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower()
            };

            var data = await Mediator.Send(query);

            return Ok(Mapper.Map<IReadOnlyCollection<BasketResponse>>(data));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBasketAsync([FromBody, Required] CreateBasketRequest request)
        {
            var command = new CreateBasketCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                UserId = request.UserId,
            };

            var data = await Mediator.Send(command);

            return Ok(Mapper.Map<BasketResponse>(data));
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
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                BasketId = basketId
            };

            var data = await Mediator.Send(query);

            return Ok(Mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddToBasketAsync([FromRoute, Required] Guid basketId, [FromBody, Required] AddToBasketRequest request)
        {
            var command = new AddToBasketCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                BasketId = basketId,
                Id = request.Id,
                Name = request.Name,
                Image = request.Image,
                Price = request.Price,
                Type = request.Type
            };

            var data = await Mediator.Send(command);

            return Ok(Mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromBasketAsync([FromRoute, Required] Guid basketId, [FromBody, Required] RemoveFromBasketRequest request)
        {
            var command = new RemoveFromBasketCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                BasketId = basketId,
                Type = request.Type,
                Id = request.Id
            };

            var data = await Mediator.Send(command);

            return Ok(Mapper.Map<BasketResponse>(data));
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
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                BasketId = basketId
            };

            var data = await Mediator.Send(command);

            return Ok(Mapper.Map<BasketResponse>(data));
        }


        [HttpPost]
        [Route("{basketid:guid}/checkout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckoutBasketAsync([FromRoute] Guid basketId)
        {
            var query = new FindBasketQuery
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                BasketId = basketId
            };

            var basket = await Mediator.Send(query);

            var products = basket.Products.Select(x => x.Id).ToList();
            var sets = basket.Sets.Select(x => x.Id).ToList();
            var command = new BasketCheckoutCommand(basketId, basket.UserId, products, sets);

            await PublishEndpoint.Publish(command);

            await _basketRepository.RemoveAsync(basket);
            return NoContent();
        }
    }
}
