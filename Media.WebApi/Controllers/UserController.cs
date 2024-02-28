using Media.Application.Features.Posts.Commands;
using Media.Application.Features.User.Commands;
using Media.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Media.WebApi.Controllers
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

        

        [HttpGet("GetProfileByName")]
        public async Task<IActionResult> GetUserByName([FromQuery] GetProfileByNameQuery request)
        {
            var users = await mediator.Send(request);
            return Ok(users);
        }

        [HttpGet("GetWhoToFollowQuery")]
        public async Task<IActionResult> GetWhoToFollowQuery([FromQuery] GetWhoToFollowQuery request)
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