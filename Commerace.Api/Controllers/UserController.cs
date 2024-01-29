using Commerace.Application;
using Commerace.Application.Dto;
using Commerace.Application.Features.AppUser;
using Media.Application;
using Media.Application.Dto;
using Media.Application.Features.AppUser;
using Media.Application.Features.Commands.Posts.DeletePost;
using Media.Application.Features.Commands.User.CreateUSer;
using Media.Application.Features.Commands.User.LoginUser;
using Media.Application.Features.Commands.User.UpdateUser;
using Media.Application.Features.Queries.Users.GetAllUsers;
using Media.Application.Features.Queries.Users.GetProfileByName;
using Media.Application.Features.Queries.Users.GetWhoToFollow;
using Media.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Commerace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetUserAndPosts")]
        public async Task<IActionResult> GetUsers([FromQuery] GetAllUsersQuery request)
        {

            List<object> users = await mediator.Send(request);

            return Ok(users);

        }

        [HttpGet("GetProfileByName")]
        public async Task<IActionResult> GetUserByName([FromQuery] GetProfileByNameQuery request)
        {
            var users = await mediator.Send(request);
            return Ok(users);
        }

        [HttpGet("GetWhoToFollowQuery")]
        public async Task<IActionResult> GetWhoToFollowQuery([FromQuery] GetWhoToFollowQueryRequest request)
        {
            var users = await mediator.Send(request);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var response = await mediator.Send(command);

            return Ok(response);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var response = await mediator.Send(command);

            return Ok(response);

        }



        [HttpPost("action")]
        public async Task<IActionResult> LoginUser(LoginUserCommand command)
        {
            var response = await mediator.Send(command);

            return Ok(response);

        }



        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommand command)
        {
            var response = await mediator.Send(command);

            return Ok(response);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(DeletePostCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var response = await mediator.Send(command);

            return Ok(response);

        }
        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand command)
        {
            var response = await mediator.Send(command);

            return Ok(response);

        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(result);

        }

    }
}
