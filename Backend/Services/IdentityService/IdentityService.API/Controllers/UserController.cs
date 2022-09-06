using AutoMapper;
using IdentityService.Application.Commands.Users;
using IdentityService.Application.Queries.Users;
using IdentityService.Contracts.v1.Contracts;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Core.Models;
using IdentityService.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<InternalIdentityUser> _userManager;
        private readonly IdentityContext _context;
        private readonly RoleManager<InternalRole> _roleManager;
        private readonly SignInManager<InternalIdentityUser> _signInManager;

        public UserController(IMapper mapper, IMediator mediator, UserManager<InternalIdentityUser> userManager, IdentityContext context, RoleManager<InternalRole> roleManager, SignInManager<InternalIdentityUser> signInManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterRequest request)
        {
            if ((request.Email != request.EmailConfirm) || (request.Password != request.Password))
            {
                return BadRequest("email or password confirmation does not match");
            }

            var command = new RegisterUserCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Firstname = request.Firstname,
                Lastname = request.Lastname
            };

            var data = await _mediator.Send(command);
            return Ok(_mapper.Map<UserResponse>(data));

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
            return Ok(_mapper.Map<UserResponse>(data));
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
            return Ok(_mapper.Map<TokenResponse>(data));
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

            await _mediator.Send(command);
            return Ok();
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
            return Ok(_mapper.Map<UserProfileResponse>(data));
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
            return Ok(_mapper.Map<UserProfileResponse>(data));
        }
    }
}
