using Commerace.Application.Dto;
using Media.Application;
using Media.Application.Features.Commands.Like.CreateLike;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerace.Api.Controllers
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
