using AutoMapper;
using IdentityService.API.Contracts.Requests;
using IdentityService.API.Contracts.Responses;
using IdentityService.Application.Commands;
using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
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
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public IdentityController(IUserService userService, IMapper mapper, IMediator mediator)
        {
            _userService = userService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("secure")]
        public async Task<IActionResult> Secure()
        {
            return Ok("Secure Data");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserRequest request)
        {
            var mapped = _mapper.Map<User>(request);

            var command = new RegisterUserCommand
            {
                User = mapped,
                Password = request.Password
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<CreateUserResponse>(data);

            return Ok(result);
        }
    }
}
