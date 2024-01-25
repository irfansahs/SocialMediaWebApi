using AutoMapper;
using Commerace.Application.Dto;
using Commerace.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Media.Application.Features.Queries.Posts.GetPostFilter
{
    public class GetPostByFilter : IRequest<List<PostViewDto>>
    {
        public string UserName { get; set; }
        public int? PostId { get; set; }
        public string? PostContent { get; set; }
        public string? ProfileName { get; set; }

        public class GetPostByFilterHandler : IRequestHandler<GetPostByFilter, List<PostViewDto>>
        {
            private readonly IPostRepository _repository;
            private readonly IMapper _mapper;

            public GetPostByFilterHandler(IPostRepository repository, IMapper mapper)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<List<PostViewDto>> Handle(GetPostByFilter request, CancellationToken cancellationToken)
            {
                var query = _repository.AsQueryable();

                query = query.Include(i => i.Comments)
                             .Include(i => i.Likes)
                             .Include(i => i.User);

                query = query.Where(i =>
                    (!request.PostId.HasValue || i.Id == request.PostId) &&
                    (string.IsNullOrEmpty(request.PostContent) || i.Content.Contains(request.PostContent)) &&
                    (string.IsNullOrEmpty(request.ProfileName) || i.User.UserName == request.ProfileName)
                );

                var postViewDtos = await query
                    .Select(i => new PostViewDto
                    {
                        Id = i.Id,
                        ProfileImage = i.User.ProfileImage,
                        Content = i.Content,
                        UserColor = i.User.UserColor,
                        UserName = i.User.UserName,
                        LikeCount = i.Likes.Count(x => x.UserId == i.User.Id)
                    })
                    .ToListAsync(cancellationToken);

                var viewmodels = _mapper.Map<List<PostViewDto>>(postViewDtos);

                return viewmodels;
            }
        }
    }

}
