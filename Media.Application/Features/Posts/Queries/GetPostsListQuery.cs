using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;
using Media.Application.Features.Posts.Dtos;
using Media.Domain.Entities;
using Media.Persistence.Page;
using Media.Application.Requests;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Routing;


namespace Media.Application.Features.Posts.Queries
{
    public class GetPostsListQuery : IRequest<PostListModel>
    {
        public string UserId { get; set; }
        public PageRequest PageRequest { get; set; }
        //  public Dynamic dynamic { get; set; }

        public class GetPostsListQueryHandler : IRequestHandler<GetPostsListQuery, PostListModel>
        {
            private readonly IPostRepository _repository;

            private readonly IMapper _mapper;

            public GetPostsListQueryHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<PostListModel> Handle(GetPostsListQuery request, CancellationToken cancellationToken)
            {
                var posts = _repository.AsQueryable().Include(c => c.Likes).Include(c => c.User);
                IPaginate<Post> paginatedPosts = _repository.GetList(
                    include: t => t.Include(c => c.Comments).Include(c => c.Likes).Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    orderBy: query => query.OrderByDescending(post => post.CreatedOn)
                );

                var postListModel = _mapper.Map<PostListModel>(paginatedPosts);

                foreach (var postDto in postListModel.items)
                {
                    postDto.IsLiked = posts.AsQueryable().Any(c => c.Likes.Any(i => i.UserId == request.UserId && i.PostId == postDto.Id));
                    postDto.IsFollow = posts.AsQueryable().Any(c => c.User.Followers.Any(f => f.FollowingId == request.UserId && c.UserId == postDto.UserId));
                }

                return postListModel;
            }

        }
    }
}