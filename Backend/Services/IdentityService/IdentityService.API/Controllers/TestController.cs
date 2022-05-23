using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("anonymous")]
        public async Task<IActionResult> Insecure()
        {
            return Ok("this resource is accessible by all requests");
        }

        [HttpGet]
        [Authorize]
        [Route("authenticated")]
        public async Task<IActionResult> Secure()
        {
            return Ok("this resource is accessible to all authenticated users");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("authorized")]
        public async Task<IActionResult> Admin()
        {
            return Ok("this resource is accessible to all authorized admin users");
        }
    }
}
