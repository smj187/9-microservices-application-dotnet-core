using AutoMapper;
using BuildingBlocks.Extensions.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Commands;
using TenantService.Application.Queries;
using TenantService.Contracts.v1.Contracts;
using TenantService.Infrastructure.Data;

namespace TenantService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenantController : ApiBaseController<TenantController>
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTenantInformationAsync()
        {
            var query = new GetTenantInformationQuery();

            var data = await Mediator.Send(query);
            return Ok(Mapper.Map<TenantResponse>(data));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateTenantAsync([FromBody, Required] CreateTenantRequest request)
        {
            var command = new CreateTenantCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Street = request.Street,
                City = request.City,
                Country = request.Country,
                Zip = request.Zip,
                State = request.State,
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<TenantResponse>(data));
        }


        [HttpPatch]
        [Route("{tenantid:guid}/add-workingday")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddTenantWorkingdayAsync([FromRoute, Required] Guid tenantId, [FromBody, Required] AddTenantWorkingdayRequest request)
        {
            var command = new AddWorkingdayToTenantCommand
            {
                Workingday = request.Workingday,
                ClosingHour = request.ClosingHour,
                ClosingMinute = request.ClosingMinute,
                OpeningHour = request.OpeningHour,
                OpeningMinute = request.OpeningMinute,
                TenantId = tenantId
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<TenantResponse>(data));
        }

        [HttpPatch]
        [Route("{tenantid:guid}/remove-workingday")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTenantWorkingdayAsync([FromRoute, Required] Guid tenantId, [FromBody, Required] RemoveTenantWorkingdayRequest request)
        {
            var command = new RemoveWorkingdayFromTenantCommand
            {
                Weekday = request.Workingday,
                TenantId = tenantId
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<TenantResponse>(data));
        }


        [HttpPatch]
        [Route("{tenantid:guid}/address")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeTenantAddressAsync([FromRoute, Required] Guid tenantId, [FromBody, Required] PatchTenantAddressRequest request)
        {
            var command = new PatchTenantAddressCommand
            {
                TenantId = tenantId,

                Street = request.Street,
                City = request.City,
                Country = request.Country,
                State = request.State,
                Zip = request.Zip,
            };


            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<TenantResponse>(data));
        }

        [HttpPatch]
        [Route("{tenantid:guid}/information")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeTenantInformationAsync([FromRoute, Required] Guid tenantId, [FromBody, Required] PatchTenantInformationRequest request)
        {
            var command = new PatchTenantInformationCommand
            {
                TenantId = tenantId,

                DeliveryCost = request.DeliveryCost,
                Description = request.Description,
                Email = request.Email,
                Imprint = request.Imprint,
                IsFreeDelivery = request.IsFreeDelivery,
                MinimunOrderAmount = request.MinimunOrderAmount,
                Name = request.Name,
                Payments = request.Payments,
                Phone = request.Phone,
                WebsiteUrl = request.WebsiteUrl,
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<TenantResponse>(data));
        }
    }
}
