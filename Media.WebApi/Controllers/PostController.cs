using Media.Application.Features.Posts.Commands;
using Media.Application.Features.Posts.Queries;
using Media.Application.Features.Tags.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Media.WebApi.Controllers
{
        [ApiController]
    [Route("api/[controller]")]
    //  [Authorize(AuthenticationSchemes = "Admin")]

    public class PostController : ControllerBase
    {
        private readonly IMediator mediator;
        public PostController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        
        [HttpGet("GetPostAndComments")]
        public async Task<IActionResult> GetPostAndComments([FromQuery] GetPostAndCommentsQuery request)
        {
            var post = await mediator.Send(request);

            return Ok(post);
        }

        [HttpGet("GetPostByDynamicQuery")]
        public async Task<IActionResult> GetPostByFilter([FromQuery] GetPostByDynamicQuery request)
        {
            var post = await mediator.Send(request);

            return Ok(post);

        }


        [HttpGet("GetTrends")]
        public async Task<IActionResult> GetTrends([FromQuery] GetTrendsQuery request)
        {
            var Post = await mediator.Send(request);

            return Ok(Post);

        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostCommand command)
        {
            return Ok(await mediator.Send(command));
        }


        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(DeletePostCommand command)
        {
            return Ok(await mediator.Send(command));
        }


    }
}