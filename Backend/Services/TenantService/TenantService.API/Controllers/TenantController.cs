using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TenantController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TenantController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<TenantResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListTenantsAsync()
        {
            var query = new ListTenantsQuery();

            var data = await _mediator.Send(query);
            return Ok(data);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateTenantAsync([FromBody, Required] CreateTenantRequest request)
        {
            var command = new CreateTenantCommand
            {
                Name = request.Name,
                Description = request.Description,
                Email = request.Email,
                Phone = request.Phone,
                Street = request.Street,
                City = request.City,
                Country = request.Country,
                Zip = request.Zip,
                State = request.State,
                DeliveryCost = request.DeliveryCost,
                Imprint = request.Imprint,
                IsFreeDelivery = request.IsFreeDelivery,
                MinimunOrderAmount = request.MinimunOrderAmount,
                WebsiteUrl = request.WebsiteUrl,
            };

            var data = await _mediator.Send(command);


            return Ok(data);
        }

        [HttpGet]
        [Route("{tenantid:guid}/find")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListTenantsAsync([FromRoute, Required] Guid tenantId)
        {
            var query = new FindTenantQuery
            {
                TenantId = tenantId
            };

            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpPatch]
        [Route("{tenantid:guid}/description")]
        public async Task<IActionResult> PatchTenantDescription([FromRoute, Required] Guid tenantId, [FromBody, Required] PatchTenantDescription request)
        {
            var command = new PatchTenantDescriptionCommand
            {
                TenantId = tenantId,
                Name = request.Name,
                Description = request.Description
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }


        [HttpPatch]
        [Route("{tenantid:guid}/add-workingday")]
        public async Task<IActionResult> AddTenantWorkingdayAsync([FromRoute, Required] Guid tenantId, [FromBody, Required] AddTenantWorkingdayRequest request)
        {
            var command = new AddWorkingdayCommand
            {
                Weekday = request.Workday,
                ClosingHour = request.ClosingHour,
                ClosingMinute = request.ClosingMinute,
                OpeningHour = request.OpeningHour,
                OpeningMinute = request.OpeningMinute,
                TenantId = tenantId
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }

        [HttpPatch]
        [Route("{tenantid:guid}/remove-workingday")]
        public async Task<IActionResult> RemoveTenantWorkingdayAsync([FromRoute, Required] Guid tenantId, [FromRoute, Required] RemoveTenantWorkingdayRequest request)
        {
            var command = new RemoveWorkingdayCommand
            {
                Weekday = request.Workday,
                TenantId = tenantId
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }


        [HttpPatch]
        [Route("{tenantid:guid}/address")]
        public async Task<IActionResult> ChangeTenantAddressAsync([FromRoute] Guid tenantId, [FromBody] PatchTenantAddressRequest request)
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


            var data = await _mediator.Send(command);
            return Ok(data);
        }
    }
}
