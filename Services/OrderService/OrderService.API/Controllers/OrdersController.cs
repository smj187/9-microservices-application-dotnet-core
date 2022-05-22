using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public OrdersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest createOrderRequest)
        {
            var mapped = _mapper.Map<Order>(createOrderRequest);

            var result = _mapper.Map<OrderResponse>(mapped);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ListOrdersAsync()
        {
            var data = new List<Order>();

            var result = _mapper.Map<IEnumerable<Order>>(data);
            return Ok(result);
        }
    }
}
