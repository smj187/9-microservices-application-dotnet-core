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
            //var mapped = _mapper.Map<Order>(createOrderRequest);

            //var command = new CreateOrderCommand
            //{
            //    NewOrder = mapped
            //};

            //var data = await _mediator.Send(command);

            //var result = _mapper.Map<OrderResponse>(data);
            //return Ok(result);

            //var orderId = Guid.Parse("6272a465-ec7d-4dbf-b3f7-ad9b80000000");
            var orderId = Guid.NewGuid();
            var user = Guid.Parse("7ef12325-13e1-48c3-bb2c-e3f4979c1337");
            var items = request.Products;

            await _publishEndpoint.Publish(new CreateOrderSagaEvent(orderId, user, items));

            return Ok(new
            {
                id = orderId
            });
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
