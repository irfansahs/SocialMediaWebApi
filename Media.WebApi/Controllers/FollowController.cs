using Media.Application.Features.Follow.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Media.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IMediator mediator;
        public FollowController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateFollow(CreateFollowCommand request)
        {
            var Follow = await mediator.Send(request);

            return Ok(Follow);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFollow(DeleteFollowCommand request)
        {
            var Follow = await mediator.Send(request);
            return Ok(Follow);
        }

    }
}