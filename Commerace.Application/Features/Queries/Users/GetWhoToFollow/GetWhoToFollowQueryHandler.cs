using AutoMapper;
using Media.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Features.Queries.Users.GetWhoToFollow
{
    public class GetWhoToFollowQueryHandler : IRequestHandler<GetWhoToFollowQueryRequest, List<GetWhoToFollowQueryResponse>>
    {
        readonly UserManager<Media.Domain.Identity.AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetWhoToFollowQueryHandler(IMapper mapper, UserManager<Media.Domain.Identity.AppUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<GetWhoToFollowQueryResponse>> Handle(GetWhoToFollowQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users.Include(i => i.Followers).AsQueryable();

            var userDto = await query
                .Where(i => i.Id != request.UserId && !i.Followers.Any(f => f.FollowerId == request.UserId))
                .Select(i => new GetWhoToFollowQueryResponse
                {
                    UserName = i.UserName,
                    ProfileImage = i.ProfileImage,
                    UserColor = i.UserColor,
                    IsFollow = i.Followers.Any(x => x.FollowerId == i.Id),
                })
                .ToListAsync(cancellationToken);

            var viewmodel = _mapper.Map<List<GetWhoToFollowQueryResponse>>(userDto);

            return userDto;
        }
    }
}
