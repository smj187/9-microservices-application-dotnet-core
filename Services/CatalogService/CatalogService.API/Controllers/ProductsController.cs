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
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListProductsAsync()
        {
            var query = new ListProductsQuery();
            var data = await _mediator.Send(query);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Price = request.Price,
            };
            var command = new CreateProductCommand
            {
                NewProduct = newProduct
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }
    }
}
