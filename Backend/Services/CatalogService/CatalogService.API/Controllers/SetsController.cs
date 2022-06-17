using BuildingBlocks.Controllers;
using CatalogService.Application.Commands.Sets;
using CatalogService.Application.Queries.Sets;
using CatalogService.Contracts.v1.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class SetsController : ApiBaseController<SetsController>
    {
        [HttpGet]
        public async Task<IActionResult> ListSetsAsync()
            =>  Ok(Mapper.Map<IReadOnlyCollection<SetResponse>>(await Mediator.Send(new ListSetsQuery())));

        [HttpPost]
        public async Task<IActionResult> CreateSetAsync([FromBody] CreateSetRequest request)
        {
            var data = await Mediator.Send(new CreateSetCommand
            {
                Name = request.Name,
                Description = request.Description,
                PriceDescription = request.PriceDescription,
                Price = request.Price,
                Quantity = request.Quantity,
                Tags = request.Tags
            });

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
