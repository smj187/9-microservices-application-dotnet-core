using AutoMapper;
using IdentityService.API.Contracts.Requests;
using IdentityService.API.Contracts.Responses;
using IdentityService.Application.Commands;
using IdentityService.Application.Queries;
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
            SetRefreshTokenInCookie(result.RefreshToken);
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

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refresh = Request.Cookies["refreshToken"];

            var command = new RefreshTokenCommand
            {
                OldToken = refresh
            };

            var data = await _mediator.Send(command);

            var result = _mapper.Map<AuthenticateUserResponse>(data);
            SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh-token/{userId:guid}")]
        public async Task<IActionResult> GetRefreshTokensAsync([FromRoute] Guid userId)
        {
            var query = new ListUserTokensQuery
            {
                UserId = userId
            };

            var data = await _mediator.Send(query);
            return Ok(data);
        }

        [HttpPost]
        [Route("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenRequest request)
        {
            var token = request.JsonWebToken ?? Request.Cookies["refreshToken"] ?? null;

            if (token == null)
                return BadRequest(new { message = "No token was provided" });

            var command = new RevokeTokenCommand
            {
                JsonWebToken = request.JsonWebToken,
                UserId = request.UserId
            };

            var data = await _mediator.Send(command);

            if(!data)
            {
                return NotFound(new { message = "Not token was found" });
            }

            return Ok(new { message = "Token revoked" });
        }


        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
