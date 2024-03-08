using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;
using Media.Application.Features.Posts.Dtos;
using Media.Domain.Entities;
using Media.Persistence.Page;
using Media.Application.Requests;
using Microsoft.EntityFrameworkCore;


namespace Media.Application.Features.Posts.Queries
{
    public class GetPostsById : IRequest<PostListModel>
    {
        public string UserId { get; set; }
        public PageRequest PageRequest { get; set; }
        //  public Dynamic dynamic { get; set; }

        public class GetPostsByIdHandler : IRequestHandler<GetPostsById, PostListModel>
        {
            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;

            public GetPostsByIdHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<PostListModel> Handle(GetPostsById request, CancellationToken cancellationToken)
            {

                IPaginate<Post> paginatedPosts = _repository.GetList(
                    a => a.UserId == request.UserId,
                    include: t => t.Include(c => c.Comments).Include(c => c.Likes).Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    orderBy: query => query.OrderByDescending(post => post.CreatedOn)
                );

                PostListModel postListModel = _mapper.Map<PostListModel>(paginatedPosts);

                return postListModel;
            }

        }
    }
}