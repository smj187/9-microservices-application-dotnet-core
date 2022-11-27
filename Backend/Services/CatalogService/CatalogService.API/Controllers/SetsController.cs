using BuildingBlocks.Attributes;
using BuildingBlocks.Cache;
using BuildingBlocks.Extensions.Controllers;
using BuildingBlocks.Extensions.strings;
using CatalogService.Application.Commands.Sets;
using CatalogService.Application.DTOs;
using CatalogService.Application.Queries.Products;
using CatalogService.Application.Queries.Sets;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Sets;
using EasyCaching.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SetsController : ApiBaseController<SetsController>
    {
        /// <summary>
        /// Returns a paginated list of all sets
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedSetResponse))]
        public async Task<IActionResult> ListSetsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            var key = $"{tenantId}_list_sets_{page}_{pageSize}";

            string? cache = await DistributedCache.GetStringAsync(key);

            PaginatedSetResponseDTO? response;
            if (string.IsNullOrEmpty(cache))
            {
                response = await Mediator.Send(new ListSetsQuery
                {
                    Page = page,
                    PageSize = pageSize
                });

                await DistributedCache.SetStringAsync(key, JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60 * 10)
                });
            }
            else
            {
                response = JsonSerializer.Deserialize<PaginatedSetResponseDTO>(cache);
            }

            return Ok(Mapper.Map<PaginatedSetResponse>(response));
        }

        /// <summary>
        /// Creates a new set
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSetAsync([FromBody] CreateSetRequest request)
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            DistributedCache.ClearCacheByPattern(Configuration, $"{tenantId}_list_sets_*");

            var data = await Mediator.Send(new CreateSetCommand
            {
                TenantId = tenantId,
                Name = request.Name,
                Description = request.Description,
                PriceDescription = request.PriceDescription,
                Price = request.Price,
                Quantity = request.Quantity,
                Tags = request.Tags
            });


            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Returns a single set
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{setid:guid}/find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindSetAsync([FromRoute] Guid setId)
        {
            var data = await Mediator.Send(new FindSetQuery
            {
                SetId = setId
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the sets price
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{setid:guid}/price")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeSetriceAsync([FromRoute] Guid setId, [FromBody] PatchSetPriceRequest request)
        {
            var data = await Mediator.Send(new PatchSetPriceCommand
            {
                SetId = setId,
                Price = request.Price
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the sets description
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{setid:guid}/description")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeSetDescriptionAsync([FromRoute] Guid setId, [FromBody] PatchSetDescriptionRequest request)
        {
            var data = await Mediator.Send(new PatchSetDescriptionCommand
            {
                SetId = setId,
                Name = request.Name,
                Description = request.Description,
                PriceDescription = request.PriceDescription,
                Tags = request.Tags
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the sets visibility
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{setid:guid}/visibility")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeSetVisibilityAsync([FromRoute] Guid setId, [FromBody] PatchSetVisibilityRequest request)
        {
            var data = await Mediator.Send(new PatchSetVisibilityCommand
            {
                SetId = setId,
                IsVisible = request.IsVisible
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the sets availability
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{setid:guid}/availability")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeSetAvailability([FromRoute] Guid setId, [FromBody] PatchSetAvailabilityRequest request)
        {
            var data = await Mediator.Send(new PatchSetAvailabilityCommand
            {
                SetId = setId,
                IsAvailable = request.IsAvailable
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the sets quantity
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{setid:guid}/quantity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeSetQuantityAsync([FromRoute] Guid setId, [FromBody] PatchSetQuantityRequest request)
        {
            var data = await Mediator.Send(new PatchSetQuantityCommand
            {
                SetId = setId,
                Quantity = request.Quantity,
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Adds a product to a given set
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{setid:guid}/add-product/{productid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddProductToSetAsync([FromRoute] Guid setId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new AddProductToSetCommand
            {
                SetId = setId,
                ProductId = productId
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }

        /// <summary>
        /// Removes a product from a given set
        /// </summary>
        /// <param name="setId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{setid:guid}/remove-product/{productid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveProductFromSetAsync([FromRoute] Guid setId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new RemoveProductFromSetCommand
            {
                SetId = setId,
                ProductId = productId
            });

            return Ok(Mapper.Map<SetDetailsResponse>(data));
        }
    }
}