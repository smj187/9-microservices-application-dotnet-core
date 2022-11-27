using BuildingBlocks.Extensions.Controllers;
using IdentityService.Application.Commands.Auth;
using IdentityService.Contracts.v1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : ApiBaseController<AuthController>
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterRequest request)
        {
            if ((request.Email != request.EmailConfirm) || (request.Password != request.Password))
            {
                return BadRequest("email or password confirmation does not match");
            }

            var command = new RegisterNewUserCommand
            {
                TenantId = HttpContext.Request.Headers["tenant-id"].ToString().ToLower(),
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Firstname = request.FirstName,
                Lastname = request.LastName
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<UserResponse>(data));
        }

        /// <summary>
        /// Login an existing user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginRequest request)
        {
            var command = new LoginExistingUserCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<UserResponse>(data));
        }

        /// <summary>
        /// Refreshes the users token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{userid:guid}/refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshTokenAsync([FromRoute] Guid userId, [FromBody] RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand
            {
                Token = request.Token,
                UserId = userId
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<TokenResponse>(data));
        }

        /// <summary>
        /// Revokes the users refresh token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{userid:guid}/revoke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RevokeTokenAsync([FromRoute] Guid userId, [FromBody] RevokeTokenRequest request)
        {
            var command = new RevokeTokenCommand
            {
                Token = request.Token,
                UserId = userId
            };

            await Mediator.Send(command);
            return Ok();
        }
    }
}