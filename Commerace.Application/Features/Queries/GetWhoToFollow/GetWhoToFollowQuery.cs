using AutoMapper;
using Commerace.Application.Dto;
using Commerace.Application.Features.Queries.GetAllProducts;
using Commerace.Application;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerace.Application.Features.Queries.GetAllUsers;

namespace Media.Application.Features.Queries.GetWhoToFollow
{
    public class GetWhoToFollowQuery : IRequest<List<PostViewDto>>
    {
        public class GetWhoToFollowQueryHandler : IRequestHandler<GetWhoToFollowQuery, List<PostViewDto>>
        {

            private readonly IPostRepository _repository;
            private readonly ILikeRepository _Likerepository;
            private readonly ICommentRepository _commentRepository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;

            private readonly IMapper _mapper;


            public GetWhoToFollowQueryHandler(IPostRepository repository, IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager, ILikeRepository likerepository, ICommentRepository commentRepository)
            {
                _commentRepository = commentRepository;
                _userManager = userManager;
                _mapper = mapper;
                _repository = repository;
                _Likerepository = likerepository;
            }


            public async Task<List<PostViewDto>> Handle(GetWhoToFollowQuery request, CancellationToken cancellationToken)
            {

                var users = await _repository.GetAllAsync();

                // Kullanıcıları rastgele sırala ve ilk 3'ü seç
                var selectedUsers = users.OrderBy(u => Guid.NewGuid()).Take(3).ToList();

                var viewmodel = _mapper.Map<List<PostViewDto>>(selectedUsers);

                foreach (var post in viewmodel)
                {
                    post.ProfileImage = _userManager.Users.Where(x => x.UserName == post.UserName).FirstOrDefault()?.ProfileImage;
                }

                return viewmodel;
            }
        }
    }
}
