using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Application;
using Media.Application.Dto;
using Media.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Queries.Posts.GetPostById
{
    public class GetPostById : IRequest<PostViewDto>
    {
        public string UserId { get; set; }
        public int PostId { get; set; }

        public class GetPostByIdHandler : IRequestHandler<GetPostById, PostViewDto>
        {

            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;

            public GetPostByIdHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }


            public async Task<PostViewDto> Handle(GetPostById request, CancellationToken cancellationToken)
            {

                var query = _repository.AsQueryable();

                query = query.Include(i => i.Comments)
                             .Include(i => i.Likes)
                             .Include(i => i.User);

                var postViewDto = await query
                    .Where(i => i.Id == request.PostId)
                    .Select(i => new PostViewDto
                    {
                        Id = i.Id,
                        ProfileImage = i.User.ProfileImage,
                        Content = i.Content,
                        UserColor = i.User.UserColor,
                        UserName = i.User.UserName,
                        LikeCount = i.Likes.Count(x => x.PostId == i.Id),
                        CommentsCount = i.Comments.Count(x => x.PostId == i.Id),
                        IsLiked = i.Likes.Any(x => x.Post.UserId == i.User.Id)
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                var viewmodel = _mapper.Map<PostViewDto>(postViewDto);

                return viewmodel;
            }
        }
    }
}
