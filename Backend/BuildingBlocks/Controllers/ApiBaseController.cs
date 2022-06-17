using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Controllers
{
    [ApiController]
    public class ApiBaseController<T> : ControllerBase where T : ApiBaseController<T>
    {
        private ILogger<T>? _logger;
        private IMediator? _mediator;
        private IMapper? _mapper;
        private IPublishEndpoint? _publishEndpoint;

        protected ILogger<T> Logger
            => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();

        protected IMediator Mediator
            => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected IMapper Mapper
            => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

        protected IPublishEndpoint PublishEndpoint
            => _publishEndpoint ??= HttpContext.RequestServices.GetRequiredService<IPublishEndpoint>();
    }
}
