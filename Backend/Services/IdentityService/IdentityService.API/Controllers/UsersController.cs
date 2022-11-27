using BuildingBlocks.Attributes;
using BuildingBlocks.Extensions.Controllers;
using IdentityService.Application.Commands.User;
using IdentityService.Application.Queries;
using IdentityService.Contracts.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController : ApiBaseController<UsersController>
    {
        /// <summary>
        /// Returns a single user profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least user-based authentication", RequiredUserRole = "User")]
        [HttpGet]
        [Route("{userid:guid}/profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserProfileAsync([FromRoute] Guid userId)
        {
            var query = new FindUserProfileQuery
            {
                UserId = userId
            };

            var data = await Mediator.Send(query);
            return Ok(Mapper.Map<UserProfileResponse>(data));
        }

        /// <summary>
        /// Patches the user's profile information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires at least user-based authentication", RequiredUserRole = "User")]
        [HttpPatch]
        [Route("{userid:guid}/profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchUserProfileAsync([FromRoute] Guid userId, [FromBody] PatchUserProfileRequest request)
        {
            var command = new PatchUserProfileCommand
            {
                UserId = userId,
                Firstname = request.FirstName,
                Lastname = request.LastName
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<UserProfileResponse>(data));
        }

        /// <summary>
        /// Returns a single user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires admin authentication", RequiredUserRole = "Administrator")]
        [HttpGet]
        [Route("find/{userid:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindUserAsync([FromRoute] Guid userId)
        {
            var query = new FindUserQuery
            {
                UserId = userId
            };

            var data = await Mediator.Send(query);
            return Ok(Mapper.Map<AdminUserResponse>(data));
        }

        /// <summary>
        /// Returns a paginated list of all users
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires admin authentication", RequiredUserRole = "Administrator")]
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListUsersAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var query = new ListUsersQuery
            {
                Page = page,
                PageSize = pageSize
            };

            var data = await Mediator.Send(query);
            return Ok(Mapper.Map<PaginatedUserResponse>(data));
        }

        /// <summary>
        /// Adds a role to a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires admin authentication", RequiredUserRole = "Administrator")]
        [HttpPatch]
        [Route("{userid:guid}/roles/add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddRolesToUserAsync([FromRoute] Guid userId, [FromBody] AddRoleToUserRequest request)
        {
            var command = new AddRoleToUserCommand
            {
                UserId = userId,
                Roles = request.Roles
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<AdminUserResponse>(data));
        }

        /// <summary>
        /// Removes a role form a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires admin authentication", RequiredUserRole = "Administrator")]
        [HttpPatch]
        [Route("{userid:guid}/roles/remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveRolesFromUser([FromRoute] Guid userId, [FromBody] RemoveRoleFromUserRequest request)
        {
            var command = new RemoveRoleFromUserCommand
            {
                UserId = userId,
                Roles = request.Roles
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<AdminUserResponse>(data));
        }

        /// <summary>
        /// Locks a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires admin authentication", RequiredUserRole = "Administrator")]
        [HttpPatch]
        [Route("{userid:guid}/lock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LockUserAccountAsync([FromRoute] Guid userId)
        {
            var command = new LockUserAccountCommand
            {
                UserId = userId
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<AdminUserResponse>(data));
        }

        /// <summary>
        /// Unlocks a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [GatewayAuthentication("Requires admin authentication", RequiredUserRole = "Administrator")]
        [HttpPatch]
        [Route("{userid:guid}/unlock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnlockUserAccountAsync([FromRoute] Guid userId)
        {
            var command = new UnlockUserAccountCommand
            {
                UserId = userId
            };

            var data = await Mediator.Send(command);
            return Ok(Mapper.Map<AdminUserResponse>(data));
        }
    }
}
