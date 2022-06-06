using AutoMapper;
using IdentityService.Application.Commands.Users;
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
            return Ok(_mapper.Map<RegisterUserResponse>(data));
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
            return Ok(_mapper.Map<LoginUserResponse>(data));
        }

        [HttpPost]
        [Route("{userid:guid}/token-refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromRoute] Guid userId, [FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand
            {
                Token = request.Token,
                UserId = userId
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<LoginUserResponse>(data));
        }

        [HttpPost]
        [Route("{userid:guid}/token-revoke")]
        public async Task<IActionResult> RevokeTokenAsync([FromRoute] Guid userId, [FromBody] RevokeTokenRequest request)
        {
            var command = new RevokeTokenCommand
            {
                Token = request.Token,
                UserId = userId
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<LoginUserResponse>(data));
        }


        [HttpGet]
        [Route("{userid:guid}/profile")]
        public async Task<IActionResult> GetUserProfileAsync([FromRoute] Guid userId)
        {
            var query = new FindUserProfileQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);
            return Ok(_mapper.Map<UserResponse>(data));
        }

        [HttpPatch]
        [Route("{userid:guid}/profile")]
        public async Task<IActionResult> PatchUserProfileAsync([FromRoute] Guid userId, [FromBody] PatchUserProfileRequest request)
        {
            var command = new PatchUserProfileCommand
            {
                UserId = userId,
                Firstname = request.Firstname,
                Lastname = request.Lastname
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<UserResponse>(data));
        }
    }
}
