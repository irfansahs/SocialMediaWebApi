using AutoMapper;
using Commerace.Application.Dto;
using Commerace.Application;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace Media.Application.Features.Queries.GetPostByContent
{
    public class GetPostByContentQuery : IRequest<List<PostViewDto>>
    {
        public string? UserName { get; set; }
        public string? Content { get; set; }

        public class GetPostByContentQueryHandler : IRequestHandler<GetPostByContentQuery, List<PostViewDto>>
        {

            private readonly IPostRepository _repository;
            private readonly ILikeRepository _LikeRepository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;
            private readonly IMapper _mapper;
            private readonly ICommentRepository _commentRepository;
            public GetPostByContentQueryHandler(IPostRepository repository, IMapper mapper, ILikeRepository likeRepository, UserManager<Media.Domain.Identity.AppUser> userManager, ICommentRepository commentRepository)
            {
                _mapper = mapper;
                _repository = repository;
                _LikeRepository = likeRepository;
                _userManager = userManager;
                _commentRepository = commentRepository;
            }
            public async Task<List<PostViewDto>> Handle(GetPostByContentQuery request, CancellationToken cancellationToken)
            {

                var posts = await _repository.GetAllAsync();

                var filteredPosts = posts
                    .Where(post => post.Content.Contains(request.Content))
                    .ToList();

                var viewmodel = _mapper.Map<List<PostViewDto>>(filteredPosts);

                foreach (var post in viewmodel)
                {
                    post.CommentsCount = await _commentRepository.GetCommentsCount(post.Id);
                    post.LikeCount = await _LikeRepository.GetLikesCount(post.Id);
                    post.IsLiked = await _LikeRepository.GetLiked(post.Id, request.UserName);
                    post.ProfileImage = _userManager.Users
                        .Where(x => x.UserName == post.UserName)
                        .FirstOrDefault()?.ProfileImage;
                }

                return viewmodel;
            }
        }
    }
}
