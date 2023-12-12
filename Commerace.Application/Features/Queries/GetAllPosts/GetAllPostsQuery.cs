using AutoMapper;
using Commerace.Application.Dto;
using Media.Application;
using MediatR;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllProducts
{
    public class GetAllPostsQuery : IRequest<List<PostViewDto>>
    {
        public string? UserName { get; set; }

        public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<PostViewDto>>
        {

            private readonly IPostRepository _repository;
            private readonly ILikeRepository _LikeRepository;
            private readonly IMapper _mapper;


            public GetAllPostsQueryHandler(IPostRepository repository, IMapper mapper, ILikeRepository likeRepository)
            {
                _mapper = mapper;
                _repository = repository;
                _LikeRepository = likeRepository;
            }

            public async Task<List<PostViewDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
            {
                var posts = await _repository.GetAllAsync();

                var viewmodel = _mapper.Map<List<PostViewDto>>(posts);

                foreach (var post in viewmodel)
                {
                    post.LikeCount = await _LikeRepository.GetLikesCount(post.Id);
                    post.IsLiked = await _LikeRepository.GetLiked(post.Id, request.UserName);
                }

                return viewmodel;
            }
        }
    }
}
