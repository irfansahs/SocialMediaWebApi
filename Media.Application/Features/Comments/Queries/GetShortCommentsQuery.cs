using AutoMapper;
using Media.Application.Features.Comments.Dtos;
using Media.Application.Features.Posts.Dtos;
using Media.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Media.Application.Features.Comments.Queries
{
    public class GetShortCommentsQuery : IRequest<List<CommentsViewDto>>
    {
        public string UserId { get; set; }
        public int PostId { get; set; }

        public class GetShortCommentsQueryHandler : IRequestHandler<GetShortCommentsQuery, List<CommentsViewDto>>
        {

            private readonly ICommentRepository _repository;
            private readonly IMapper _mapper;

            public GetShortCommentsQueryHandler(ICommentRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<List<CommentsViewDto>> Handle(GetShortCommentsQuery request, CancellationToken cancellationToken)
            {
                var query = _repository.AsQueryable();

                query = query.Include(i => i.Likes)
                             .Include(i => i.User);


                var postAndCommentsDto = await query
                    .Where(i => i.PostId == request.PostId)
                    .Select(i => new CommentsViewDto
                    {
                        Id = i.Id,
                        Content = i.Content,
                        Emotion = i.Emotion,
                        Polarity = i.Polarity,
                        IsLiked = i.Likes.Any(x => x.Comment.UserId == request.UserId && x.Id == i.Id),
                        LikeCount = i.Likes.Count(x => x.CommentId == i.Id),
                        UserName = i.User.UserName,
                        ProfileImage = i.User.ProfileImage,
                        CreatedOn = i.CreatedOn,
                        SourceLanguageCode = i.SourceLanguageCode
                    })
                    .OrderByDescending(i => i.CreatedOn)
                    .Take(3)
                    .ToListAsync(cancellationToken);

                var viewmodel = _mapper.Map<List<CommentsViewDto>>(postAndCommentsDto);

                return viewmodel;
            }

        }

    }
}