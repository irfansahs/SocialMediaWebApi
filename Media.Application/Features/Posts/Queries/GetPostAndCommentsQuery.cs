using AutoMapper;
using Media.Application.Features.Comments.Dtos;
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
                        IsLiked = i.Likes.Any(x => x.Post.UserId == request.UserId && i.Id == x.PostId),
                        IsFollow = i.User.Followers.Any(x => x.FollowerId == request.UserId && i.UserId == x.FollowingId),
                        Emotion = i.Emotion,
                        Polarity = Convert.ToString(Decimal.Round(Decimal.Parse(i.Polarity), 2)),
                        CreatedOn = i.CreatedOn,
                        SourceLanguageCode = i.SourceLanguageCode,
                        Comments = i.Comments.Select(c => new CommentsViewDto
                        {
                            Id = c.Id,
                            Content = c.Content,
                            Emotion = c.Emotion,
                            Polarity = c.Polarity,
                            IsLiked = c.Likes.Any(x => x.UserId == request.UserId && x.CommentId == c.Id),
                            IsFollow = c.User.Followers.Any(x => x.FollowerId == request.UserId && c.UserId == x.FollowingId),
                            LikeCount = c.Likes.Count(x => x.CommentId == c.Id),
                            UserName = c.User.UserName,
                            ProfileImage = c.User.ProfileImage,
                            CreatedOn = c.CreatedOn,
                            SourceLanguageCode = c.SourceLanguageCode,
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                var viewmodel = _mapper.Map<PostAndComments>(postAndCommentsDto);

                return viewmodel;
            }

        }

    }
}