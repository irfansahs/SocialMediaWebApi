using AutoMapper;
using Commerace.Application.Dto;
using Media.Application;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            private readonly ICommentRepository _commentRepository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;
            private readonly IMapper _mapper;


            public GetAllPostsQueryHandler(IPostRepository repository, IMapper mapper, ILikeRepository likeRepository, UserManager<Media.Domain.Identity.AppUser> userManager, ICommentRepository commentRepository)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
                _repository = repository;
                _LikeRepository = likeRepository;
                _userManager = userManager;
            }

            public async Task<List<PostViewDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
            {
                var posts = await _repository.GetAllAsync();

                posts = posts.OrderByDescending(p => p.CreatedOn).ToList();

                var viewmodel = _mapper.Map<List<PostViewDto>>(posts);

                foreach (var post in viewmodel)
                {
                    post.CommentsCount = await _commentRepository.GetCommentsCount(post.Id);
                    post.LikeCount = await _LikeRepository.GetLikesCount(post.Id);
                    post.IsLiked = await _LikeRepository.GetLiked(post.Id, request.UserName);
                    post.ProfileImage = _userManager.Users.Where(x => x.UserName == post.UserName).FirstOrDefault()?.ProfileImage;
                }

                return viewmodel;
            }
        }
    }
}
