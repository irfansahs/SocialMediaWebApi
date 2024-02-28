using Media.Application.Features.Likes.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Media.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LİkeController : ControllerBase
    {
        private readonly IMediator mediator;
        public LİkeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLike(CreateLikeCommand request)
        {
            var Like = await mediator.Send(request);

            return Ok(Like);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLike(DeleteLikeCommand request)
        {
            var Like = await mediator.Send(request);

            return Ok(Like);

        }

    }
}