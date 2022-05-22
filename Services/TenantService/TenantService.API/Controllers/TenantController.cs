using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Application.Commands;
using TenantService.Application.Queries;
using TenantService.Contracts.v1.Requests;
using TenantService.Contracts.v1.Responses;
using TenantService.Core.Entities;
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
        public async Task<IActionResult> ListTenantsAsync()
        {
            var query = new ListTenantsQuery();

            var data = await _mediator.Send(query);

            var result = _mapper.Map<IEnumerable<TenantResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenantAsync([FromBody] CreateTenantRequest createTenantRequest)
        {
            var mapped = _mapper.Map<Tenant>(createTenantRequest);
            var command = new CreateTenantCommand
            {
                NewTenant = mapped
            };
  
            var data = await _mediator.Send(command);

            var result = _mapper.Map<TenantResponse>(data);
            return Ok(result);
        }
    }
}
