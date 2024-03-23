using Media.Application.Features.Comments.Commands;
using Media.Application.Features.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Media.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly IMediator mediator;
        public CommentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetShortCommentsQuery([FromQuery] GetShortCommentsQuery request)
        {
            var comments = await mediator.Send(request);

            return Ok(comments);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComments([FromBody] CreateCommentCommand request)
        {
            var comments = await mediator.Send(request);

            return Ok(comments);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComments([FromBody] DeleteCommentCommand request)
        {
            var comments = await mediator.Send(request);

            return Ok(comments);
        }


    }
}