using AutoMapper;
using Media.Application.Features.User.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Media.Application.Features.User.Queries
{
     public class GetWhoToFollowQuery : IRequest<UserResponseDto>
    {
          public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string UserColor { get; set; }
        public bool IsFollow { get; set; }
        public string UserId { get; private set; }


        public class GetWhoToFollowQueryHandler : IRequestHandler<GetWhoToFollowQuery, UserResponseDto>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Media.Domain.Entities.Identity.AppUser> _userManager;

            public GetWhoToFollowQueryHandler(IMapper mapper, UserManager<Media.Domain.Entities.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<UserResponseDto> Handle(GetWhoToFollowQuery request, CancellationToken cancellationToken)
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