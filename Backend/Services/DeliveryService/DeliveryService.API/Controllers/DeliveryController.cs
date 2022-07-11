using AutoMapper;
using BuildingBlocks.Extensions.Controllers;
using DeliveryService.Application.Queries;
using DeliveryService.Contracts.v1.Requests;
using DeliveryService.Core.Domain.Aggregates;
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
    public class DeliveryController : ApiBaseController<DeliveryController>
    {
        [HttpGet]
        public async Task<IActionResult> ListDeliveriesAsync()
        {
            var query = new ListDeliveryQuery();

            var data = await Mediator.Send(query);
            return Ok(data);
            //var result = _mapper.Map<IReadOnlyCollection<Delivery>>(data);
            //return Ok(result);
        }

        [HttpGet]
        [Route("{deliveryid:guid}")]
        public async Task<IActionResult> FindDeliveryAsync([FromRoute] Guid deliveryId)
        {
            var query = new FindDeliveryQuery
            {
                DeliveryId = deliveryId
            };

            var data = await Mediator.Send(query);
            return Ok(data);
        }

      
    }
}
