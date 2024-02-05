using AutoMapper;
using Commerace.Application;
using Commerace.Application.Dto;
using Media.Application;
using Media.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Queries.Posts.GetAllPosts
{
    public class GetAllPostsQuery : IRequest<List<PostViewDto>>
    {
        public string UserId { get; set; }

        public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<PostViewDto>>
        {

            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;


            public GetAllPostsQueryHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<List<PostViewDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
            {

                var query = _repository.AsQueryable();

                query = query.Include(i => i.Comments)
                             .Include(i => i.Likes)
                             .Include(i => i.User);


                var list = await query.Select(i => new PostViewDto()
                {
                    Id = i.Id,
                    ProfileImage = i.User.ProfileImage,
                    Content = i.Content,
                    UserColor = i.User.UserColor,
                    UserName = i.User.UserName,
                    LikeCount = i.Likes.Count(x => x.PostId == i.Id),
                    CommentsCount = i.Comments.Count(x=>x.PostId == i.Id),
                    IsLiked = i.Likes.Any(x=>x.Post.UserId == i.User.Id)
                }).ToListAsync();

                var viewmodel = _mapper.Map<List<PostViewDto>>(list);

                return viewmodel;
            }
        }
    }
}
