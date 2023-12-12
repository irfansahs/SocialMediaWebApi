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
    public class GetPostById : IRequest<PostViewDto>
    {
        public string UserName { get; set; }
        public int PostId { get; set; }

        public class GetPostByIdHandler : IRequestHandler<GetPostById, PostViewDto>
        {

            private readonly IPostRepository _repository;
            private readonly ILikeRepository _Likerepository;

            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;

            private readonly IMapper _mapper;


            public GetPostByIdHandler(IPostRepository repository, IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager, ILikeRepository likerepository)
            {
                _userManager = userManager;
                _mapper = mapper;
                _repository = repository;
                _Likerepository = likerepository;   
            }


            public async Task<PostViewDto> Handle(GetPostById request, CancellationToken cancellationToken)
            {

                var posts = await _repository.GetByIdAsync(request.PostId);
                var likes = await _Likerepository.GetLikesCount(request.PostId);
                var isliked = await _Likerepository.GetLiked(request.PostId,request.UserName);
                
                var viewmodel = _mapper.Map<PostViewDto>(posts);
                
                viewmodel.LikeCount = likes;
                viewmodel.IsLiked = isliked;

                return viewmodel;
            }
        }
    }
}
