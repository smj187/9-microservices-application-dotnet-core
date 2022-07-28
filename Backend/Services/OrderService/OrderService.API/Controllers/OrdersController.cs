using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderService.Application.Commands;
using OrderService.Application.Queries;
using OrderService.Application.StateMachines;
using OrderService.Application.StateMachines.Events;
using OrderService.Application.StateMachines.Responses;
using OrderService.Contracts.v1;
using OrderService.Contracts.v1.Requests;
using OrderService.Contracts.v1.Responses;
using OrderService.Core.Entities.Aggregates;
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
        private readonly IRequestClient<CheckOrderStatusEvent> _client;
        public OrdersController(IMapper mapper, IMediator mediator, IRequestClient<CheckOrderStatusEvent> client)
        {
            _mapper = mapper;
            _mediator = mediator;
            _client = client;
        }

        [HttpGet]
        [Route("{orderid:guid}/status")]
        public async Task<IActionResult> CheckOrderStatusAsync([FromRoute] Guid orderId)
        {
            var (status, notFound) = await _client.GetResponse<CheckOrderStatusResponse, OrderNotFoundResponse>(new CheckOrderStatusEvent(orderId));
            if(status.IsCompletedSuccessfully)
            {
                var response = await status;
                return Ok(response);
            }
            var res = await notFound;
            return NotFound(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            var command = new InitializeSagaCommand
            {
                OrderId = Guid.NewGuid(),
                //OrderId = Guid.Parse("58ea4c2e-0000-0000-0000-28c4c1d5e40e"),
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                BasketId = request.BasketId,
                Products = request.Products,
                Sets = request.Sets,
                UserId = request.UserId
            };

            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpGet]
        [Route("{orderid:guid}")]
        public async Task<IActionResult> FindOrderAsync([FromRoute] Guid orderId)
        {
            var query = new FindOrderQuery
            {
                OrderId = orderId,
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower()
            };

            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpGet]
        [Route("active-orders")]
        public async Task<IActionResult> ListActiveOrdersAsync()
        {
            var query = new ListActiveOrdersQuery
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower()
            };

            var data = await _mediator.Send(query);
            return Ok(data);
            //var result = _mapper.Map<IEnumerable<Order>>(data);
        }

        [HttpGet]
        [Route("complete-orders")]
        public async Task<IActionResult> ListCompleteOrdersAsync()
        {
            var query = new ListCompleteOrdersQuery
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower()
            };

            var data = await _mediator.Send(query);
            return Ok(data);
        }
    }
}
