using CatalogService.Contracts.v1.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IRequestClient<RequestRequestEvent> _requestClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public TestController(IRequestClient<RequestRequestEvent> requestClient, IPublishEndpoint publishEndpoint)
        {
            _requestClient = requestClient;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = new RequestRequestEvent
            {
                Id = Guid.Parse("a94ba49a-8e4c-47ac-b10f-1a6958fea4b8")
            };

            var data2 = new PublishEvent
            {
                Id = Guid.Parse("a94ba49a-8e4c-47ac-b10f-1a6958fea4b8"),
                Message = "!!!"
            };

            await _publishEndpoint.Publish<PublishEvent>(data2);

            var re = _requestClient.Create(data);
            var response = await _requestClient.GetResponse<RequestResponseEvent>(data);

            Console.WriteLine($"-> {response.Message.Echo}");

            return Ok(response);

        }

    }
}
