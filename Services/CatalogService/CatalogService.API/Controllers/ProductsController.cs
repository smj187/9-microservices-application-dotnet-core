using AutoMapper;
using CatalogService.API.Contracts.Requests;
using CatalogService.API.Contracts.Responses;
using CatalogService.Application.Commands;
using CatalogService.Application.Queries;
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

            var result = _mapper.Map<IEnumerable<ProductResponse>>(data);
            return Ok(result);
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
