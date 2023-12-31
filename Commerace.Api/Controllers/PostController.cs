﻿using Bogus;
using Commerace.Application;
using Commerace.Application.Dto;
using Commerace.Application.Features.Queries.GetAllProducts;
using Commerace.Infrastructure;
using Media.Application.Features.Commands.Posts.CreatePost;
using Media.Application.Features.Commands.Posts.DeletePost;
using Media.Application.Features.Commands.User.CreateProduct;
using Media.Application.Features.Queries.GetPostByContent;
using Media.Application.Features.Queries.GetTrends;
using Media.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Commerace.Api.Controllers
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


        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] GetAllPostsQuery request)
        {

            List<PostViewDto> Product = await mediator.Send(request);

            return Ok(Product);

        }
        [HttpGet("GetPostByContent")]
        public async Task<IActionResult> GetPostByContent([FromQuery] GetPostByContentQuery request)
        {
            List<PostViewDto> Product = await mediator.Send(request);

            return Ok(Product);

        }

        [HttpGet("GetAllPostsByUserName")]
        public async Task<IActionResult> GetAllPostsByUserName([FromQuery] GetPostByUserName request)
        {
            var Post = await mediator.Send(request);
            return Ok(Post);

        }
        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById([FromQuery] GetPostById request)
        {
            var post = await mediator.Send(request);

            var response = post != null ? new[] { post } : Array.Empty<PostViewDto>();

            return Ok(response);

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
