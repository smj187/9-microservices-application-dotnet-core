using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.Queries;
using OrderService.Application.StateMachines.Events;
using OrderService.Application.StateMachines.Responses;
using OrderService.Contracts.v1;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<CheckOrderStatusEvent> _client;

        public OrdersController(IMapper mapper, IMediator mediator, IPublishEndpoint publishEndpoint, IRequestClient<CheckOrderStatusEvent> client)
        {
            _mapper = mapper;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
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
            var command = new CreateOrderSagaEvent(Guid.NewGuid(), request.UserId, request.TenantId, request.Products, request.Sets);
            await _publishEndpoint.Publish(command);
            return Ok(command);
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
