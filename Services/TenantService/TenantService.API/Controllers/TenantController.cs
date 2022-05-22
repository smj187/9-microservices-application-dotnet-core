using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Contracts.v1.Requests;
using TenantService.Contracts.v1.Responses;
using TenantService.Core.Entities;

namespace TenantService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly IMapper _mapper;

        public TenantController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> ListTenantsAsync()
        {
            var data = new List<Tenant>();

            var result = _mapper.Map<IEnumerable<TenantResponse>>(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenantAsync([FromBody] CreateTenantRequest createTenantRequest)
        {
            var data = _mapper.Map<Tenant>(createTenantRequest);

            var result = _mapper.Map<TenantResponse>(data);
            return Ok(result);
        }
    }
}
