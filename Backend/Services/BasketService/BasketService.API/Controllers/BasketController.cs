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
            => Ok(Mapper.Map<IReadOnlyCollection<BasketResponse>>(await Mediator.Send(new ListBasketsQuery())));


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBasketAsync([FromBody, Required] CreateBasketRequest request)
        {
            var data = await Mediator.Send(new CreateBasketCommand
            {
                UserId = request.UserId,
            });
            return Ok(Mapper.Map<BasketResponse>(data)); ;
        }


        [HttpGet]
        [Route("{basketid:guid}/find-basket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindBasketAsync([FromRoute, Required] Guid basketId)
        {
            var data = await Mediator.Send(new FindBasketQuery
            {
                BasketId = basketId
            });
            return Ok(Mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddToBasketAsync([FromRoute, Required] Guid basketId, [FromBody, Required] AddToBasketRequest request)
        {
            var data = await Mediator.Send(new AddToBasketCommand
            {
                BasketId = basketId,
                Id = request.Id,
                Name = request.Name,
                Image = request.Image,
                Price = request.Price,
                Type = request.Type
            });
            return Ok(Mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromBasketAsync([FromRoute, Required] Guid basketId, [FromBody, Required] RemoveFromBasketRequest request)
        {
            var data = await Mediator.Send(new RemoveFromBasketCommand
            {
                BasketId = basketId,
                Type = request.Type,
                Id = request.Id
            });
            return Ok(Mapper.Map<BasketResponse>(data));
        }


        [HttpPatch]
        [Route("{basketid:guid}/clear")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ClearBasketAsync([FromRoute, Required] Guid basketId)
        {
            var data = await Mediator.Send(new ClearBasketCommand
            {
                BasketId = basketId
            });
            return Ok(Mapper.Map<BasketResponse>(data));
        }


        [HttpPost]
        [Route("{basketid:guid}/checkout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckoutBasketAsync([FromRoute] Guid basketId)
        {
            var basket = await Mediator.Send(new FindBasketQuery
            {
                BasketId = basketId
            });

            var products = basket.Products.Select(x => x.Id).ToList();
            var sets = basket.Sets.Select(x => x.Id).ToList();
            var command = new BasketCheckoutCommand(basketId, basket.UserId, products, sets);

            await PublishEndpoint.Publish(command);

            await _basketRepository.RemoveAsync(basket);
            return NoContent();
        }
    }
}
