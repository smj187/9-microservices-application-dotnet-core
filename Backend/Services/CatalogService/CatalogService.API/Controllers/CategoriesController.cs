using AutoMapper;
using CatalogService.Application.Commands;
using CatalogService.Application.Commands.Categories;
using CatalogService.Application.Queries;
using CatalogService.Application.Queries.Categories;
using CatalogService.Contracts.v1;
using CatalogService.Core.Entities;
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
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListCategoriesAsync()
        {
            var query = new ListCategoryQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<CategoryResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest request)
        {
            var mapped = _mapper.Map<Category>(request);
            var command = new CreateCategoryCommand
            {
                NewCategory = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CategoryResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        [Route("{categoryid:guid}/find")]
        public async Task<IActionResult> FindCategoryAsync([FromRoute] Guid categoryId)
        {
            var query = new FindCategoryQuery
            {
                CategoryId = categoryId
            };

            var data = await _mediator.Send(query);

            var result = _mapper.Map<CategoryResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{categoryid:guid}/visibility")]
        public async Task<IActionResult> ChangeCategoryVisibilityAsync([FromRoute] Guid categoryId, [FromBody] PatchCategoryVisibilityCommand request)
        {
            var command = new PatchCategoryVisibilityCommand
            {
                CategoryId = categoryId,
                IsVisible = request.IsVisible
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CategoryResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{categoryid:guid}/add-product/{productid:guid}")]
        public async Task<IActionResult> AddProductToCategoryAsync([FromRoute] Guid categoryId, [FromRoute] Guid productId)
        {
            var command = new AddProductToCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CategoryResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{categoryid:guid}/remove-product/{productid:guid}")]
        public async Task<IActionResult> RemoveProductFromCategoryAsync([FromRoute] Guid categoryId, [FromRoute] Guid productId)
        {
            var command = new RemoveProductFromCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CategoryResponse>(data);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{categoryid:guid}/description")]
        public async Task<IActionResult> ChangeCategoryDescriptionAsync([FromRoute] Guid categoryId, [FromBody] PatchCategoryDescriptionRequest request)
        {
            var command = new PatchCategoryDescriptionCommand
            {
                CategoryId = categoryId,
                Name = request.Name,
                Description = request.Description,
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CategoryResponse>(data);
            return Ok(result);
        }

    }
}
