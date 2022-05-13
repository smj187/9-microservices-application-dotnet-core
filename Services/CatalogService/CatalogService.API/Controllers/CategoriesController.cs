using CatalogService.API.Contracts.Requests;
using CatalogService.Application.Commands;
using CatalogService.Application.Queries;
using CatalogService.Core.Models;
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

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListCategoriesAsync()
        {
            var query = new ListCategoriesQuery();
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoriesAsync([FromBody] CreateCategoryRequst request)
        {
            var newProduct = new Category
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
            };
            var command = new CreateCategoryCommand
            {
                NewCategory = newProduct
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }
    }
}
