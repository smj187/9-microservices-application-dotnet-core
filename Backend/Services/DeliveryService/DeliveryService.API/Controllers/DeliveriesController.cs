using AutoMapper;
using DeliveryService.Application.Commands;
using DeliveryService.Application.Queries;
using DeliveryService.Contracts.v1.Requests;
using DeliveryService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeliveriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListDeliveriesAsync()
        {
            var query = new ListDeliveriesQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IReadOnlyCollection<Delivery>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoriesAsync([FromBody] CreateDeliveryRequest createCategoryRequest)
        {
            var mapped = _mapper.Map<Delivery>(createCategoryRequest);
            var command = new CreateDeliveryCommand
            {
                NewDelivery = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<Delivery>(data);
            return Ok(result);
        }
    }
}
