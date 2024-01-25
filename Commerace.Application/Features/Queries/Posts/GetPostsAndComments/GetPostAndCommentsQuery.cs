using AutoMapper;
using Commerace.Application.Dto;
using Commerace.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Media.Application.Dto;
using Microsoft.EntityFrameworkCore;

namespace Media.Application.Features.Queries.Posts.GetPostsAndComments
{
    public class GetPostAndCommentsQuery : IRequest<PostAndComments>
    {
        public string UserName { get; set; }
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
                             .Include(i => i.Comments); // Bu satırı ekleyerek Comments koleksiyonunu dahil edin

                var postAndCommentsDto = await query
                    .Where(i => i.Id == request.PostId)
                    .Select(i => new PostAndComments
                    {
                        Id = i.Id,
                        ProfileImage = i.User.ProfileImage,
                        Content = i.Content,
                        UserColor = i.User.UserColor,
                        UserName = i.User.UserName,
                        LikeCount = i.Likes.Count(x => x.UserId == i.User.Id),
                        CommentsCount = i.Comments.Count, // Yorum sayısını ekleyin
                        Comments = i.Comments // Yorumları ekleyin
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                var viewmodel = _mapper.Map<PostAndComments>(postAndCommentsDto);

                return viewmodel;
            }

        }

    }
}
