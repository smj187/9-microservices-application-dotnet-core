using AutoMapper;
using CatalogService.Application.Commands;
using CatalogService.Application.Queries;
using CatalogService.Contracts.v1.Requests;
using CatalogService.Contracts.v1.Responses;
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

            var result = _mapper.Map<IReadOnlyCollection<Category>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoriesAsync([FromBody] CreateCategoryRequest createCategoryRequest)
        {
            var mapped = _mapper.Map<Category>(createCategoryRequest);
            var command = new CreateCategoryCommand
            {
                NewCategory = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CategoryResponse>(data);
            return Ok(result);
        }
    }
}
