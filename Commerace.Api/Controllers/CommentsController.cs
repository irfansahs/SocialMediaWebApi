using Commerace.Application.Dto;
using Commerace.Application.Features.Queries.GetAllComments;
using Commerace.Application.Features.Queries.GetAllProducts;
using Media.Application.Features.Commands.Posts.CreatePost;
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


        [HttpGet]
        public async Task<IActionResult> GetAllComments([FromQuery] GetAllCommentsQuery request)
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


    }
}
