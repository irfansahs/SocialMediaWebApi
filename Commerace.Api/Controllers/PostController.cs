using Bogus;
using Commerace.Application;
using Commerace.Application.Dto;
using Commerace.Application.Features.Queries.GetAllProducts;
using Commerace.Infrastructure;
using Media.Application.Features.Commands.Posts.CreatePost;
using Media.Application.Features.Commands.Posts.DeletePost;
using Media.Application.Features.Commands.User.CreateProduct;
using Media.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Commerace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //  [Authorize(AuthenticationSchemes = "Admin")]

    public class PostController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserDbContext userContext;
        public PostController(IMediator mediator, UserDbContext userDbContext)
        {
            this.mediator = mediator;
            this.userContext = userDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] GetAllPostsQuery request)
        {

            List<PostViewDto> Product = await mediator.Send(request);

            return Ok(Product);

        }
        [HttpGet("GetAllPostAndCommand")]
        public async Task<IActionResult> GetAllPostssss()
        {
            var posts = userContext.Posts.Include(p => p.Comments).ToList();

            return Ok(posts);

        }
        [HttpGet("GetAllUserPosts")]
        public async Task<IActionResult> GetAllUserPosts(string username)
        {
            var posts = userContext.Posts.Include(p => p.Comments).ToList();

            var users = userContext.AppUsers.ToList();

            var userposts = users.FirstOrDefault(s => s.UserName == username);


            return Ok(userposts);

        }


        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostCommand command)
        {
            return Ok(await mediator.Send(command));
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePost(DeletePostCommand command)
        {
            return Ok(await mediator.Send(command));
        }

      


    }
}
