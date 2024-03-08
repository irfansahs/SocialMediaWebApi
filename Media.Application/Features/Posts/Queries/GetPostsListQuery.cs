using AutoMapper;
using Media.Application.Services.Repositories;
using MediatR;
using Media.Application.Features.Posts.Dtos;
using Media.Domain.Entities;
using Media.Persistence.Page;
using Media.Application.Requests;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


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
            private readonly ILikeRepository _likerepository;
            
            private readonly IMapper _mapper;

            public GetPostsListQueryHandler(IPostRepository repository, IMapper mapper,ILikeRepository likeRepository)
            {
                _mapper = mapper;
                _repository = repository;
                _likerepository = likeRepository;
            }

            public async Task<PostListModel> Handle(GetPostsListQuery request, CancellationToken cancellationToken)
            {
                // Tüm postları çek
                IPaginate<Post> paginatedPosts = _repository.GetList(
                    include: t => t.Include(c => c.Comments).Include(c => c.Likes).Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    orderBy: query => query.OrderByDescending(post => post.CreatedOn)
                );

                // PostListModel'e dönüştür
                var postListModel = _mapper.Map<PostListModel>(paginatedPosts);

                // IsLiked alanını ayarla
                foreach (var postDto in postListModel.items)
                {
                    postDto.IsLiked = _likerepository.AsQueryable().Any(like => like.UserId == request.UserId && like.PostId == postDto.Id);
                }

                return postListModel;
            }

        }
    }
}