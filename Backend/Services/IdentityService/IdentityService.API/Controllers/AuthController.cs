using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Auth()
        {
            Console.WriteLine("check auth...");
            Console.WriteLine($"{Request.Headers}");
            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["authorization"]);
                var credentials = header.Parameter;

                return Ok(credentials);
            }
            catch (Exception ex)
            {
                return Unauthorized("no");
            }
        }
    }
}