using BuildingBlocks.Extensions.Controllers;
using BuildingBlocks.Extensions.Strings;
using CatalogService.Application.Commands.Products;
using CatalogService.Application.Queries.Products;
using CatalogService.Contracts.v1.Contracts;
using CatalogService.Core.Domain.Products;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{

    [Route("api/v1/[controller]")]
    public class ProductsController : ApiBaseController<ProductsController>
    {
        private readonly IRedisCachingProvider _cache;

        public ProductsController(IEasyCachingProviderFactory factory, IConfiguration configuration)
        {
            _cache = factory.GetRedisProvider(configuration.GetValue<string>("Cache:Database"));
        }


        [HttpGet]
        public async Task<IActionResult> ListProductsAsync()
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            var key = $"{tenantId}_{nameof(ListProductsAsync).ToSnakeCase()}";
            var cache = _cache.StringGet(key);

            IReadOnlyCollection<Product>? response = null;

            //if (cache != null)
            //{
            //    response = JsonConvert.DeserializeObject<List<Product>>(cache);
            //}
            //else
            //{
                response = await Mediator.Send(new ListProductsQuery());
                //_cache.StringSet(key, JsonConvert.SerializeObject(response), TimeSpan.FromSeconds(60 * 15));
            //}

            return Ok(Mapper.Map<IReadOnlyCollection<ProductResponse>>(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            var data = await Mediator.Send(new CreateProductCommand
            {
                NewProduct = Mapper.Map<Product>(request)
            });

            _cache.KeyDel(nameof(ListProductsAsync).ToSnakeCase());

            return Ok(Mapper.Map<ProductResponse>(data));
        }

        [HttpGet]
        [Route("{productid:guid}/find")]
        public async Task<IActionResult> FindProductAsync([FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new FindProductQuery
            {
                ProductId = productId
            });
            return Ok(Mapper.Map<ProductResponse>(data));
        }


        [HttpPatch]
        [Route("{productid:guid}/price")]
        public async Task<IActionResult> ChangePriceAsync([FromRoute] Guid productId, [FromBody] PatchProductPriceRequest request)
        {
            var data = await Mediator.Send(new PatchProductPriceCommand
            {
                ProductId = productId,
                Price = request.Price
            });
            return Ok(Mapper.Map<ProductResponse>(data));
        }

        [HttpPatch]
        [Route("{productid:guid}/description")]
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
            return Ok(Mapper.Map<ProductResponse>(data));
        }

        [HttpPatch]
        [Route("{productid:guid}/visibility")]
        public async Task<IActionResult> ChangeVisibilityAsync([FromRoute] Guid productId, [FromBody] PatchProductVisibilityRequest request)
        {
            var data = await Mediator.Send(new PatchProductVisibilityCommand
            {
                ProductId = productId,
                IsVisible = request.IsVisible
            });
            return Ok(Mapper.Map<ProductResponse>(data));
        }

        [HttpPatch]
        [Route("{productid:guid}/availability")]
        public async Task<IActionResult> ChangeAvailabilityAsync([FromRoute] Guid productId, [FromBody] PatchProductAvailabilityRequest request)
        {
            var data = await Mediator.Send(new PatchProductAvailabilityCommand
            {
                ProductId = productId,
                IsAvailable = request.IsAvailable
            });
            return Ok(Mapper.Map<ProductResponse>(data));
        }

        [HttpPatch]
        [Route("{productid:guid}/quantity")]
        public async Task<IActionResult> ChangeQuantityAsync([FromRoute] Guid productId, [FromBody] PatchProductQuantityRequest request)
        {
            var data = await Mediator.Send(new PatchProductQuantityCommand
            {
                ProductId = productId,
                Quantity = request.Quantity,
            });
            return Ok(Mapper.Map<ProductResponse>(data));
        }

        [HttpPatch]
        [Route("{productid:guid}/ingredient/add")]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid productId, [FromBody] AddIngredientsRequest request)
        {
            var data = await Mediator.Send(new AddIngredientsToProductCommand
            {
                ProductId = productId,
                Allergens = Mapper.Map<List<Allergen>>(request.Allergens),
                Ingredients = Mapper.Map<List<Ingredient>>(request.Ingredients),
                Nutritions = Mapper.Map<List<Nutrition>>(request.Nutritions),
            });
            return Ok(Mapper.Map<ProductResponse>(data));
        }

  

        [HttpPatch]
        [Route("{productid:guid}/ingredient/remove")]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid ProductId, [FromBody] RemoveIngredientsRequest request)
        {
            var data = await Mediator.Send(new RemoveIngredientsFromProductCommand
            {
                ProductId = ProductId,
                Allergens = Mapper.Map<List<Allergen>>(request.Allergens),
                Ingredients = Mapper.Map<List<Ingredient>>(request.Ingredients),
                Nutritions = Mapper.Map<List<Nutrition>>(request.Nutritions),
            });
            return Ok(Mapper.Map<ProductResponse>(data));
        }
    }
}
