using AutoMapper;
using CatalogService.Application.Commands;
using CatalogService.Application.Queries;
using CatalogService.Contracts.v1.Requests;
using CatalogService.Contracts.v1.Responses;
using CatalogService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Route("list")]
        public async Task<IActionResult> ListProductsAsync()
        {
            var query = new ListProductsQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<ProductResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        [Route("create")]
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
    }
}
