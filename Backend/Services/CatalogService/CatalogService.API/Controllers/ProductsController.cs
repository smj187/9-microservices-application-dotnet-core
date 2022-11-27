using BuildingBlocks.Attributes;
using BuildingBlocks.Cache;
using BuildingBlocks.Extensions.Controllers;
using BuildingBlocks.Extensions.strings;
using BuildingBlocks.Mongo.Helpers;
using CatalogService.Application.Commands.Products;
using CatalogService.Application.DTOs;
using CatalogService.Application.Queries.Products;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Products;
using EasyCaching.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace CatalogService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductsController : ApiBaseController<ProductsController>
    {
        /// <summary>
        /// Returns a paginated list of all products
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedProductResponse))]
        public async Task<IActionResult> ListProductsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            var key = $"{tenantId}_list_products_{page}_{pageSize}";

            string? cache = await DistributedCache.GetStringAsync(key);


            PaginatedProductResponseDTO? response;
            if (string.IsNullOrEmpty(cache))
            {
                response = await Mediator.Send(new ListProductsQuery
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
                response = JsonSerializer.Deserialize<PaginatedProductResponseDTO>(cache);
            }

            return Ok(Mapper.Map<PaginatedProductResponse>(response));
        }

        /// <summary>
        /// Creates a new product and deletes all cached product list queries
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Administrator")]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            DistributedCache.ClearCacheByPattern(Configuration, $"{tenantId}_list_products_*");

            var data = await Mediator.Send(new CreateProductCommand
            {
                NewProduct = Mapper.Map<Product>(request)
            });

            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Returns a single product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productid:guid}/find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindProductAsync([FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new FindProductQuery
            {
                ProductId = productId
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the products price
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{productid:guid}/price")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePriceAsync([FromRoute] Guid productId, [FromBody] PatchProductPriceRequest request)
        {
            var data = await Mediator.Send(new PatchProductPriceCommand
            {
                ProductId = productId,
                Price = request.Price
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the products description
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{productid:guid}/description")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeDescriptionAsync([FromRoute] Guid productId, [FromBody] PatchProductDescriptionRequest request)
        {
            var data = await Mediator.Send(new PatchProductDescriptionCommand
            {
                ProductId = productId,
                Name = request.Name,
                Description = request.Description,
                PriceDescription = request.PriceDescription,
                Tags = request.Tags
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the products visibility
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{productid:guid}/visibility")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeVisibilityAsync([FromRoute] Guid productId, [FromBody] PatchProductVisibilityRequest request)
        {
            var data = await Mediator.Send(new PatchProductVisibilityCommand
            {
                ProductId = productId,
                IsVisible = request.IsVisible
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the products availability
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{productid:guid}/availability")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeAvailabilityAsync([FromRoute] Guid productId, [FromBody] PatchProductAvailabilityRequest request)
        {
            var data = await Mediator.Send(new PatchProductAvailabilityCommand
            {
                ProductId = productId,
                IsAvailable = request.IsAvailable
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Patches the products quantity
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{productid:guid}/quantity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeQuantityAsync([FromRoute] Guid productId, [FromBody] PatchProductQuantityRequest request)
        {
            var data = await Mediator.Send(new PatchProductQuantityCommand
            {
                ProductId = productId,
                Quantity = request.Quantity,
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Add ingredients to a given product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{productid:guid}/ingredient/add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid productId, [FromBody] AddIngredientsRequest request)
        {
            var data = await Mediator.Send(new AddIngredientsToProductCommand
            {
                ProductId = productId,
                Allergens = Mapper.Map<List<Allergen>>(request.Allergens),
                Ingredients = Mapper.Map<List<Ingredient>>(request.Ingredients),
                Nutritions = Mapper.Map<List<Nutrition>>(request.Nutritions),
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }

        /// <summary>
        /// Remove ingredients to a given product
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{productid:guid}/ingredient/remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid ProductId, [FromBody] RemoveIngredientsRequest request)
        {
            var data = await Mediator.Send(new RemoveIngredientsFromProductCommand
            {
                ProductId = ProductId,
                Allergens = Mapper.Map<List<Allergen>>(request.Allergens),
                Ingredients = Mapper.Map<List<Ingredient>>(request.Ingredients),
                Nutritions = Mapper.Map<List<Nutrition>>(request.Nutritions),
            });
            return Ok(Mapper.Map<ProductDetailsResponse>(data));
        }
    }
}
