﻿using BuildingBlocks.Extensions.Controllers;
using BuildingBlocks.Extensions.Strings;
using CatalogService.Application.Commands.Categories;
using CatalogService.Application.Queries.Categories;
using CatalogService.Contracts.v1.Contracts;
using CatalogService.Core.Domain.Categories;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IRedisCachingProvider _cache;

        public CategoriesController(IEasyCachingProviderFactory factory, IConfiguration configuration)
        {
            _cache = factory.GetRedisProvider(configuration.GetValue<string>("Cache:Database"));
        }

        [HttpGet]
        public async Task<IActionResult> ListCategoriesAsync()
        {
            var key = nameof(ListCategoriesAsync).ToSnakeCase();

            var cache = _cache.StringGet(key);

            IReadOnlyCollection<Category>? response = null;

            if (cache != null)
            {
                response = JsonConvert.DeserializeObject<List<Category>>(cache);
            }
            else
            {
                response = await Mediator.Send(new ListCategoryQuery());
                //_cache.StringSet(key, JsonConvert.SerializeObject(response), TimeSpan.FromSeconds(60 * 15));
            }

            return Ok(Mapper.Map<IReadOnlyCollection<CategoryResponse>>(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            var data = await Mediator.Send(new CreateCategoryCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Name = request.Name,
                Description = request.Description,
                Products = request.Products,
                Sets = request.Sets
            });

            _cache.KeyDel(nameof(ListCategoriesAsync).ToSnakeCase());


            return Ok(Mapper.Map<CategoryResponse>(data));
        }

        [HttpGet]
        [Route("{categoryid:guid}/find")]
        public async Task<IActionResult> FindCategoryAsync([FromRoute] Guid categoryId)
        {
            var data = await Mediator.Send(new FindCategoryQuery
            {
                CategoryId = categoryId
            });

            return Ok(Mapper.Map<CategoryResponse>(data));
        }

        [HttpPatch]
        [Route("{categoryid:guid}/visibility")]
        public async Task<IActionResult> ChangeCategoryVisibilityAsync([FromRoute, Required] Guid categoryId, [FromBody, Required] PatchCategoryVisibilityRequest request)
        {
            var data = await Mediator.Send(new PatchCategoryVisibilityCommand
            {
                CategoryId = categoryId,
                IsVisible = request.IsVisible
            });

            return Ok(Mapper.Map<CategoryResponse>(data));
        }

        [HttpPatch]
        [Route("{categoryid:guid}/add-product/{productid:guid}")]
        public async Task<IActionResult> AddProductToCategoryAsync([FromRoute] Guid categoryId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new AddProductToCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            });

            return Ok(Mapper.Map<CategoryResponse>(data));
        }

        [HttpPatch]
        [Route("{categoryid:guid}/remove-product/{productid:guid}")]
        public async Task<IActionResult> RemoveProductFromCategoryAsync([FromRoute] Guid categoryId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new RemoveProductFromCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            });

            return Ok(Mapper.Map<CategoryResponse>(data));
        }

        [HttpPatch]
        [Route("{categoryid:guid}/description")]
        public async Task<IActionResult> ChangeCategoryDescriptionAsync([FromRoute] Guid categoryId, [FromBody] PatchCategoryDescriptionRequest request)
        {
            var data = await Mediator.Send(new PatchCategoryDescriptionCommand
            {
                CategoryId = categoryId,
                Name = request.Name,
                Description = request.Description,
            });

            return Ok(Mapper.Map<CategoryResponse>(data));
        }

        [HttpPatch]
        [Route("{categoryid:guid}/add-set/{setid:guid}")]
        public async Task<IActionResult> AddSetToCategory([FromRoute] Guid categoryId, [FromRoute] Guid setId)
        {
            var data = await Mediator.Send(new AddSetToCategoryCommand
            {
                CategoryId = categoryId,
                SetId = setId
            });

            return Ok(Mapper.Map<CategoryResponse>(data));
        }

        [HttpPatch]
        [Route("{categoryid:guid}/remove-set/{setid:guid}")]
        public async Task<IActionResult> RemoveSetFromCategory([FromRoute] Guid categoryId, [FromRoute] Guid setId)
        {
            var data = await Mediator.Send(new RemoveSetFromCategoryCommand
            {
                CategoryId = categoryId,
                SetId = setId
            });

            return Ok(Mapper.Map<CategoryResponse>(data));
        }
    }
}
