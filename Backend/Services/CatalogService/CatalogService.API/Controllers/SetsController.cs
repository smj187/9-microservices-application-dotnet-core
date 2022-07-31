using BuildingBlocks.Extensions.Controllers;
using BuildingBlocks.Extensions.Strings;
using CatalogService.Application.Commands.Sets;
using CatalogService.Application.Queries.Sets;
using CatalogService.Contracts.v1.Contracts;
using CatalogService.Core.Domain.Sets;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace CatalogService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SetsController : ApiBaseController<SetsController>
    {
        private readonly IRedisCachingProvider _cache;

        public SetsController(IEasyCachingProviderFactory factory, IConfiguration configuration)
        {
            _cache = factory.GetRedisProvider(configuration.GetValue<string>("Cache:Database"));
        }

        [HttpGet]
        public async Task<IActionResult> ListSetsAsync()
        {
            var tenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower();
            var key = $"{tenantId}_{nameof(ListSetsAsync).ToSnakeCase()}";
            var cache = _cache.StringGet(key);

            IReadOnlyCollection<Set>? response = null;

            if (cache != null)
            {
                response = JsonConvert.DeserializeObject<List<Set>>(cache);
            }
            else
            {
                response = await Mediator.Send(new ListSetsQuery());
                _cache.StringSet(key, JsonConvert.SerializeObject(response), TimeSpan.FromSeconds(60 * 15));
            }

            return Ok(Mapper.Map<IReadOnlyCollection<SetResponse>>(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSetAsync([FromBody] CreateSetRequest request)
        {
            var data = await Mediator.Send(new CreateSetCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Name = request.Name,
                Description = request.Description,
                PriceDescription = request.PriceDescription,
                Price = request.Price,
                Quantity = request.Quantity,
                Tags = request.Tags
            });

            _cache.KeyDel(nameof(ListSetsAsync).ToSnakeCase());

            return Ok(Mapper.Map<SetResponse>(data));
        }


        [HttpGet]
        [Route("{setid:guid}/find")]
        public async Task<IActionResult> FindSetAsync([FromRoute] Guid setId)
        {
            var data = await Mediator.Send(new FindSetQuery
            {
                SetId = setId
            });

            return Ok(Mapper.Map<SetResponse>(data));
        }


        [HttpPatch]
        [Route("{setid:guid}/price")]
        public async Task<IActionResult> ChangeSetriceAsync([FromRoute] Guid setId, [FromBody] PatchSetPriceRequest request)
        {
            var data = await Mediator.Send(new PatchSetPriceCommand
            {
                SetId = setId,
                Price = request.Price
            });

            return Ok(Mapper.Map<SetResponse>(data));
        }

        [HttpPatch]
        [Route("{setid:guid}/description")]
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

            return Ok(Mapper.Map<SetResponse>(data));
        }

        [HttpPatch]
        [Route("{setid:guid}/visibility")]
        public async Task<IActionResult> ChangeSetVisibilityAsync([FromRoute] Guid setId, [FromBody] PatchSetVisibilityRequest request)
        {
            var data = await Mediator.Send(new PatchSetVisibilityCommand
            {
                SetId = setId,
                IsVisible = request.IsVisible
            });

            return Ok(Mapper.Map<SetResponse>(data));
        }

        [HttpPatch]
        [Route("{setid:guid}/availability")]
        public async Task<IActionResult> ChangeSetAvailability([FromRoute] Guid setId, [FromBody] PatchSetAvailabilityRequest request)
        {
            var data = await Mediator.Send(new PatchProductAvailabilityCommand
            {
                SetId = setId,
                IsAvailable = request.IsAvailable
            });

            return Ok(Mapper.Map<SetResponse>(data));
        }

        [HttpPatch]
        [Route("{setid:guid}/quantity")]
        public async Task<IActionResult> ChangeSetQuantityAsync([FromRoute] Guid setId, [FromBody] PatchSetQuantityRequest request)
        {
            var data = await Mediator.Send(new PatchSetQuantityCommand
            {
                SetId = setId,
                Quantity = request.Quantity,
            });

            return Ok(Mapper.Map<SetResponse>(data));
        }


        [HttpPatch]
        [Route("{setid:guid}/add-product/{productid:guid}")]
        public async Task<IActionResult> AddProductToSetAsync([FromRoute] Guid setId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new AddProductToSetCommand
            {
                SetId = setId,
                ProductId = productId
            });

            return Ok(Mapper.Map<SetResponse>(data));
        }

        [HttpPatch]
        [Route("{setid:guid}/remove-product/{productid:guid}")]
        public async Task<IActionResult> RemoveProductFromSetAsync([FromRoute] Guid setId, [FromRoute] Guid productId)
        {
            var data = await Mediator.Send(new RemoveProductFromSetCommand 
            { 
                SetId = setId, 
                ProductId = productId 
            });

            return Ok(Mapper.Map<SetResponse>(data));
        }
    }
}
