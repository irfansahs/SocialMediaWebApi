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

namespace Media.Application.Features.Queries.Users.GetWhoToFollow
{
    public class GetWhoToFollowQuery : IRequest<List<PostViewDto>>
    {
        public class GetWhoToFollowQueryHandler : IRequestHandler<GetWhoToFollowQuery, List<PostViewDto>>
        {

            readonly UserManager<Domain.Identity.AppUser> _userManager;
            private readonly IMapper _mapper;


            public GetWhoToFollowQueryHandler(IMapper mapper, UserManager<Domain.Identity.AppUser> userManager)
            {
                _userManager = userManager;
                _mapper = mapper;
            }


            public async Task<List<PostViewDto>> Handle(GetWhoToFollowQuery request, CancellationToken cancellationToken)
            {

                var users = _userManager.Users.ToList();

                var selectedUsers = users.OrderBy(u => Guid.NewGuid()).Take(3).ToList();

                var viewmodel = _mapper.Map<List<PostViewDto>>(selectedUsers);

                return viewmodel;
            }
        }
    }
}
