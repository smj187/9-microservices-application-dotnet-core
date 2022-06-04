using AutoMapper;
using IdentityService.Application.Commands;
using IdentityService.Application.Commands.Users;
using IdentityService.Application.Queries;
using IdentityService.Application.Queries.Users;
using IdentityService.Contracts.v1;
using MediatR;
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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterRequest request)
        {
            if((request.Email != request.EmailConfirm) || (request.Password != request.Password))
            {
                return BadRequest("email or password confirmation does not match");
            }

            var command = new RegisterUserCommand
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Firstname = request.Firstname,
                Lastname = request.Lastname
            };

            var data = await _mediator.Send(command);

            return Ok(data);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginRequest request)
        {
            var command = new LoginUserCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            var data = await _mediator.Send(command);
            return Ok(data);
        }


        [HttpGet]
        [Route("test")]
        [Authorize]
        public IActionResult AuthCheck() => Ok("hi");

    }
}
