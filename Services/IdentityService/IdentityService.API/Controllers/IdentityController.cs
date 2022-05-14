using AutoMapper;
using IdentityService.API.Contracts.Requests;
using IdentityService.API.Contracts.Responses;
using IdentityService.Application.Commands;
using IdentityService.Application.Services;
using IdentityService.Core.Models;
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
        [Authorize]
        [Route("secure")]
        public async Task<IActionResult> Secure()
        {
            return Ok("Secure Data");
        }    
        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("admin")]
        public async Task<IActionResult> SecureAdministrator()
        {
            return Ok("Secure Data Administrator");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserRequest request)
        {
            var mapped = _mapper.Map<User>(request);

            var command = new RegisterUserCommand
            {
                User = mapped,
                Password = request.Password,
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<AuthenticateUserResponse>(data);
            return Ok(result);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserRequest request)
        {
            var mapped = _mapper.Map<Token>(request);

            var command = new AuthenticateUserCommand
            {
                Token = mapped
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<AuthenticateUserResponse>(data);
            return Ok(result);
        }

        [HttpPost]
        [Route("promote")]
        public async Task<IActionResult> PromoteAsync([FromBody] PromoteRoleRequest request)
        {
            var command = new PromoteRoleCommand
            {
                Username = request.Username,
                NewRule = request.NewRole
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<UserResponse>(data);
            return Ok(result);
        }        
        
        [HttpPost]
        [Route("revoke")]
        public async Task<IActionResult> RevokeAsync([FromBody] RevokeRoleRequest request)
        {
            var command = new RevokeRoleCommand
            {
                Username = request.Username,
                RoleToRemove = request.RoleToRevoke
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<UserResponse>(data);
            return Ok(result);
        }
    }
}
