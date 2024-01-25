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
        public string UserName { get; set; }
        public string? ProfileName { get; set; }

        public class GetUserByNameQueryHandler : IRequestHandler<GetProfileByNameQuery, UserResponseDto>
        {

            private readonly IMapper _mapper;
            readonly UserManager<Domain.Identity.AppUser> _userManager;


            public GetUserByNameQueryHandler(IMapper mapper, UserManager<Domain.Identity.AppUser> userManager)
            {
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<UserResponseDto> Handle(GetProfileByNameQuery request, CancellationToken cancellationToken)
            {
                var query = _userManager.Users.Where(x => x.UserName == request.ProfileName).AsQueryable();

                query = query.Include(i => i.Comments)
                             .Include(i => i.Likes)
                             .Include(i => i.Followers);

                var postViewDto = await query
                    .Where(i => i.UserName == request.UserName)
                    .Select(i => new UserResponseDto
                    {
                        ProfileImage = i.ProfileImage,
                        UserColor = i.UserColor,
                        UserName = i.UserName,
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                var viewmodel = _mapper.Map<UserResponseDto>(postViewDto);

                return viewmodel;
            }
        }
    }
}
