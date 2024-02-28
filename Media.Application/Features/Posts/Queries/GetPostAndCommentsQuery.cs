using AutoMapper;
using Media.Application.Features.Posts.Dtos;
using Media.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Media.Application.Features.Posts.Queries
{
    public class GetPostAndCommentsQuery : IRequest<PostAndComments>
    {
        public string UserId { get; set; }
        public int PostId { get; set; }

        public class GetPostAndCommentsHandler : IRequestHandler<GetPostAndCommentsQuery, PostAndComments>
        {

            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;


            public GetPostAndCommentsHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<PostAndComments> Handle(GetPostAndCommentsQuery request, CancellationToken cancellationToken)
            {
                var query = _repository.AsQueryable();

                query = query.Include(i => i.Likes)
                             .Include(i => i.User)
                             .Include(i => i.Comments); 

                var postAndCommentsDto = await query
                    .Where(i => i.Id == request.PostId)
                    .Select(i => new PostAndComments
                    {
                        Id = i.Id,
                        ProfileImage = i.User.ProfileImage,
                        Content = i.Content,
                        UserColor = i.User.UserColor,
                        UserName = i.User.UserName,
                        LikeCount = i.Likes.Count(x => x.PostId == i.Id),
                        CommentsCount = i.Comments.Count(x => x.PostId == i.Id),
                        IsLiked = i.Likes.Any(x => x.Post.UserId == i.User.Id),
                        Comments = i.Comments 
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                var viewmodel = _mapper.Map<PostAndComments>(postAndCommentsDto);

                return viewmodel;
            }

        }

    }
}