using AutoMapper;
using BuildingBlocks.Extensions.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Queries;

namespace TenantService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ApiBaseController<OrderController>
    {
        [HttpGet]
        public async Task<IActionResult> ListOrdersAsync()
        {
            var query = new ListOrdersQuery();

            var data = await Mediator.Send(query);
            return Ok(data);
        }
    }
}
