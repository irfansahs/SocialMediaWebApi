using AutoMapper;
using Commerace.Application.Dto;
using Media.Application;
using Media.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Features.Queries.GetAllProducts
{
    public class GetProfileByNameQuery : IRequest<UserResponseDto>
    {
        public string UserName { get; set; }
        public string? ProfileName { get; set; }

        public class GetUserByNameQueryHandler : IRequestHandler<GetProfileByNameQuery, UserResponseDto>
        {

            private readonly IMapper _mapper;
            private readonly IFollowRepository _followRepository;
            readonly UserManager<Media.Domain.Identity.AppUser> _userManager;


            public GetUserByNameQueryHandler(IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager, IFollowRepository followRepository)
            {
                _followRepository = followRepository;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<UserResponseDto> Handle(GetProfileByNameQuery request, CancellationToken cancellationToken)
            {
                var user = _userManager.Users.Where(x => x.UserName == request.ProfileName).FirstOrDefault();

                string FollowTo = request.ProfileName;
                var FollowCount = await _followRepository.GetFollowsCount(FollowTo);

                var IsFollow = await _followRepository.GetFollowed(request.ProfileName, request.UserName);

                UserResponseDto viewmodel = _mapper.Map<UserResponseDto>(user);

                viewmodel.IsFollow = IsFollow;
                viewmodel.FollowCount = FollowCount;

                return viewmodel;
            }
        }
    }
}
