using AutoMapper;
using Commerace.Application.Dto;
using Media.Application;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllProducts
{
    public class GetPostByUserName : IRequest<List<PostViewDto>>
    {
        public string UserName { get; set; }
        

        public class GetPostByUserNameHandler : IRequestHandler<GetPostByUserName, List<PostViewDto>>
        {

            private readonly IPostRepository _repository;
            private readonly ILikeRepository _Likerepository;
            private readonly ICommentRepository _commentRepository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;

            private readonly IMapper _mapper;


            public GetPostByUserNameHandler(IPostRepository repository, IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager, ILikeRepository likerepository, ICommentRepository commentRepository)
            {
                _commentRepository = commentRepository;
                _userManager = userManager;
                _mapper = mapper;
                _repository = repository;
                _Likerepository = likerepository;
            }


            public async Task<List<PostViewDto>> Handle(GetPostByUserName request, CancellationToken cancellationToken)
            {

                var posts = await _repository.GetByNameAsync(x => x.UserName == request.UserName);

                var viewmodel = _mapper.Map<List<PostViewDto>>(posts);

                foreach (var post in viewmodel)
                {
                    post.CommentsCount = await _commentRepository.GetCommentsCount(post.Id);
                    post.LikeCount = await _Likerepository.GetLikesCount(post.Id);
                    post.IsLiked = await _Likerepository.GetLiked(post.Id, request.UserName);
                    post.ProfileImage = _userManager.Users.Where(x => x.UserName == post.UserName).FirstOrDefault()?.ProfileImage;
                }

                return viewmodel;
            }
        }
    }
}
