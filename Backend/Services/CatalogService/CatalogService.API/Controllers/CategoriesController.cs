using BuildingBlocks.Attributes;
using BuildingBlocks.Cache;
using BuildingBlocks.Extensions.Controllers;
using BuildingBlocks.Extensions.strings;
using CatalogService.Application.Commands.Categories;
using CatalogService.Application.DTOs;
using CatalogService.Application.Queries.Categories;
using CatalogService.Application.Queries.Sets;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Categories;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoriesController : ApiBaseController<CategoriesController>
    {
        /// <summary>
        /// Returns a list of all categories
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<PaginatedCategoryResponse>))]
        public async Task<IActionResult> ListCategoriesAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            var key = $"{tenantId}_list_categories_{page}_{pageSize}";

            string? cache = await DistributedCache.GetStringAsync(key);

            PaginatedCategoryResponseDTO? response;
            if (string.IsNullOrEmpty(cache))
            {
                response = await Mediator.Send(new ListCategoryQuery
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
                response = JsonSerializer.Deserialize<PaginatedCategoryResponseDTO>(cache);
            }

            return Ok(Mapper.Map<PaginatedCategoryResponse>(response));
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            DistributedCache.ClearCacheByPattern(Configuration, $"{tenantId}_list_categories_*");

            var data = await Mediator.Send(new CreateCategoryCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Name = request.Name,
                Description = request.Description,
                Products = request.Products,
                Sets = request.Sets
            });


            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }

        /// <summary>
        /// Returns a single category based on a query parameter
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{categoryid:guid}/find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindCategoryAsync([FromRoute] Guid categoryId)
        {
            var data = await Mediator.Send(new FindCategoryQuery
            {
                CategoryId = categoryId
            });

            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }

        /// <summary>
        /// Changes the category's visibility flag
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{categoryid:guid}/visibility")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeCategoryVisibilityAsync([FromRoute, Required] Guid categoryId, [FromBody, Required] PatchCategoryVisibilityRequest request)
        {
            var data = await Mediator.Send(new PatchCategoryVisibilityCommand
            {
                CategoryId = categoryId,
                IsVisible = request.IsVisible
            });

            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }

        /// <summary>
        /// Adds a product to the category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{categoryid:guid}/add-product/{productid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddProductToCategoryAsync([FromRoute] Guid categoryId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new AddProductToCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            });

            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }

        /// <summary>
        /// Removes a product from the category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{categoryid:guid}/remove-product/{productid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveProductFromCategoryAsync([FromRoute] Guid categoryId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new RemoveProductFromCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            });

            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }

        /// <summary>
        /// Changes the category's description
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{categoryid:guid}/description")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeCategoryDescriptionAsync([FromRoute] Guid categoryId, [FromBody] PatchCategoryDescriptionRequest request)
        {
            var data = await Mediator.Send(new PatchCategoryDescriptionCommand
            {
                CategoryId = categoryId,
                Name = request.Name,
                Description = request.Description,
            });

            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }

        /// <summary>
        /// Adds a set to the category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="setId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{categoryid:guid}/add-set/{setid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddSetToCategory([FromRoute] Guid categoryId, [FromRoute] Guid setId)
        {
            var data = await Mediator.Send(new AddSetToCategoryCommand
            {
                CategoryId = categoryId,
                SetId = setId
            });

            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }

        /// <summary>
        /// Removes a set from the category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="setId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least moderator-based authentication", RequiredUserRole = "Moderator")]
        [HttpPatch]
        [Route("{categoryid:guid}/remove-set/{setid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDetailsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveSetFromCategory([FromRoute] Guid categoryId, [FromRoute] Guid setId)
        {
            var data = await Mediator.Send(new RemoveSetFromCategoryCommand
            {
                CategoryId = categoryId,
                SetId = setId
            });

            return Ok(Mapper.Map<CategoryDetailsResponse>(data));
        }
    }
}