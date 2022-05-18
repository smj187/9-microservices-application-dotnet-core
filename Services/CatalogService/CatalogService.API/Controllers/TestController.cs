using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test() => Ok("this is the catalog service");
    }
}
