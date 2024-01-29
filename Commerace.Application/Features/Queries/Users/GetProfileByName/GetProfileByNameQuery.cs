using AutoMapper;
using Commerace.Application.Dto;
using Media.Application;
using Media.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Queries.Users.GetProfileByName
{
    public class GetProfileByNameQuery : IRequest<UserResponseDto>
    {
        public string UserId { get; set; }
        public string? ProfileName { get; set; }

        public class GetProfileByNameQueryHandler : IRequestHandler<GetProfileByNameQuery, UserResponseDto>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Domain.Identity.AppUser> _userManager;


            public GetProfileByNameQueryHandler(IMapper mapper, UserManager<Domain.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<UserResponseDto> Handle(GetProfileByNameQuery request, CancellationToken cancellationToken)
            {

                var query = _userManager.Users.Include(i => i.Comments)
                                              .Include(i => i.Likes)
                                              .Include(i => i.Followers)
                                              .Include(i => i.Posts)
                                              .AsQueryable();

                var userDto = await query
                    .Where(i => i.Id == request.UserId)
                    .Select(i => new UserResponseDto
                    {
                        UserName = i.UserName,
                        ProfileImage = i.ProfileImage,
                        UserColor = i.UserColor,
                        IsFollow = i.Followers.Any(x => x.FollowerId == i.Id),
                        PostsCount = i.Posts.Count(x => x.UserId == i.Id),
                        FollowCount = i.Followers.Count(x => x.FollowingId == i.Id),
                        FollowersCount = i.Followers.Count(x => x.FollowerId == i.Id),
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                var viewmodel = _mapper.Map<UserResponseDto>(userDto);

                return viewmodel;
            }
        }
    }
}
