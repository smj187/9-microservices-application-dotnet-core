using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.Queries;
using OrderService.Contracts.v1.Requests;
using OrderService.Contracts.v1.Responses;
using OrderService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrdersController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest createOrderRequest)
        {
            var mapped = _mapper.Map<Order>(createOrderRequest);

            var command = new CreateOrderCommand
            {
                NewOrder = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<OrderResponse>(data);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListOrdersAsync()
        {
            var query = new ListOrdersQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IEnumerable<Order>>(data);
            return Ok(result);
        }
    }
}
