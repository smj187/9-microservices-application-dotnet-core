using AutoMapper;
using CatalogService.Application.Commands.Products;
using CatalogService.Application.Queries.Products;
using CatalogService.Contracts.v1.Contracts;
using CatalogService.Core.Domain.Product;
using FileService.Contracts.v1;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            var mapped = _mapper.Map<Product>(request);
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
        public async Task<IActionResult> ChangePriceAsync([FromRoute] Guid productId, [FromBody] PatchProductPriceRequest request)
        {
            var command = new PatchProductPriceCommand
            {
                ProductId = productId,
                Price = request.Price
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{productid:guid}/description")]
        public async Task<IActionResult> ChangeDescriptionAsync([FromRoute] Guid productId, [FromBody] PatchProductDescriptionRequest request)
        {
            var command = new PatchProductDescriptionCommand
            {
                ProductId = productId,
                Name = request.Name,
                Description = request.Description,
                PriceDescription = request.PriceDescription,
                Tags = request.Tags
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{productid:guid}/visibility")]
        public async Task<IActionResult> ChangeVisibilityAsync([FromRoute] Guid productId, [FromBody] PatchProductVisibilityRequest request)
        {
            var command = new PatchVisibilityCommand
            {
                ProductId = productId,
                IsVisible = request.IsVisible
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{productid:guid}/ingredient/add")]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid productId, [FromBody] AddIngredientsRequest request)
        {
            var command = new AddIngredientsToProductCommand
            {
                ProductId = productId,
                Allergens = _mapper.Map<List<Allergen>>(request.Allergens),
                Ingredients = _mapper.Map<List<Ingredient>>(request.Ingredients),
                Nutritions = _mapper.Map<List<Nutrition>>(request.Nutritions),
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }

  

        [HttpPatch]
        [Route("{productid:guid}/ingredient/remove")]
        public async Task<IActionResult> AddIngredientsAsync([FromRoute] Guid ProductId, [FromBody] RemoveIngredientsRequest request)
        {
            var command = new RemoveIngredientsFromProductCommand
            {
                ProductId = ProductId,
                Allergens = _mapper.Map<List<Allergen>>(request.Allergens),
                Ingredients = _mapper.Map<List<Ingredient>>(request.Ingredients),
                Nutritions = _mapper.Map<List<Nutrition>>(request.Nutritions),
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<ProductResponse>(data);
            return Ok(result);
        }
    }
}
