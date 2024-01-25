using Commerace.Application.Dto;
using Media.Application.Features.Commands.Posts.CreatePost;
using Media.Application.Features.Commands.Posts.DeletePost;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerace.Api.Controllers
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
