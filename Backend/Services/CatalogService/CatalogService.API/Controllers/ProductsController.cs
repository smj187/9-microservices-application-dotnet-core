using AutoMapper;
using CatalogService.Application.Commands.Products;
using CatalogService.Application.Queries.Products;
using CatalogService.Contracts.v1;
using CatalogService.Core.Entities;
using CatalogService.Core.Entities.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListProductsAsync()
        {
            var query = new ListProductsQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<ProductResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest createProductRequest)
        {
            var mapped = _mapper.Map<Product>(createProductRequest);
            var command = new CreateProductCommand
            {
                NewProduct = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        [Route("{productid:guid}/find")]
        public async Task<IActionResult> FindProductAsync([FromRoute] Guid productId)
        {
            var query = new FindProductQuery
            {
                ProductId = productId
            };

            var data = await _mediator.Send(query);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }


        [HttpPatch]
        [Route("{productid:guid}/price")]
        public async Task<IActionResult> ChangePriceAsync([FromRoute] Guid productId, [FromBody] PatchProductPriceRequest changeProductPriceRequest)
        {
            var command = new PatchProductPriceCommand
            {
                ProductId = productId,
                Price = changeProductPriceRequest.Price
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{productid:guid}/description")]
        public async Task<IActionResult> ChangeDescriptionAsync([FromRoute] Guid productId, [FromBody] PatchProductDescriptionRequest changeDescriptionRequest)
        {
            var command = new PatchProductDescriptionCommand
            {
                ProductId = productId,
                Name = changeDescriptionRequest.Name,
                Description = changeDescriptionRequest.Description,
                PriceDescription = changeDescriptionRequest.PriceDescription,
                Tags = changeDescriptionRequest.Tags
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{productid:guid}/visibility")]
        public async Task<IActionResult> ChangeVisibilityAsync([FromRoute] Guid productId, [FromBody] PatchProductVisibilityRequest changeVisibilityRequest)
        {
            var command = new PatchVisibilityCommand
            {
                ProductId = productId,
                IsVisible = changeVisibilityRequest.IsVisible
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{productid:guid}/ingredient/add")]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid productId, [FromBody] AddIngredientsRequest addIngredientsRequest)
        {
            var command = new AddIngredientsToProductCommand
            {
                ProductId = productId,
                Allergens = _mapper.Map<List<Allergen>>(addIngredientsRequest.Allergens),
                Ingredients = _mapper.Map<List<Ingredient>>(addIngredientsRequest.Ingredients),
                Nutritions = _mapper.Map<List<Nutrition>>(addIngredientsRequest.Nutritions),
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{productid:guid}/ingredient/remove")]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid ProductId, [FromBody] RemoveIngredientsRequest removeIngredientsRequest)
        {
            var command = new RemoveIngredientsFromProductCommand
            {
                ProductId = ProductId,
                Allergens = _mapper.Map<List<Allergen>>(removeIngredientsRequest.Allergens),
                Ingredients = _mapper.Map<List<Ingredient>>(removeIngredientsRequest.Ingredients),
                Nutritions = _mapper.Map<List<Nutrition>>(removeIngredientsRequest.Nutritions),
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }
    }
}
